using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Parser.Disassemble;
using Parser.Helpers;
using Parser.HtmlObjects;
using Parser.Tags;

namespace Tests.Parser.Disassemble
{
    class BodyDisassemble_Tests
    {
        private ServiceProvider _provider;
        private IDisassemble _disassemble;

        private string[] lines;
        private string text;

        [OneTimeSetUp]
        public void Init()
        {
            ServiceCollection services = new ServiceCollection();

            // Disassemble
            services.AddTransient<IDisassemble, BodyDisassemble>();

            // Helpers
            services.AddSingleton<IDepth, Depth>();
            services.AddSingleton<LineModifier>();
            services.AddSingleton<Index>();
            services.AddSingleton<Seeker>();

            // HtmlObjects
            services.AddTransient<ListItem>();
            services.AddSingleton<Code>();
            services.AddSingleton<InlineCode>();
            services.AddSingleton<Text>();

            // Tags
            services.AddSingleton<ITag, TestTags>();

            _provider = services.BuildServiceProvider();
        }

        [SetUp]
        public void GetDisassemble()
        {
            _disassemble = _provider.GetService<IDisassemble>();
            _provider.GetService<ITag>().ResetId();
        }


        // is Html elements Work

        [Test]
        public void isCodeWork()
        {
            // Arrange
            lines = new string[] { "\tnano other text", "\ttext","not code" };
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual("<code>nano other text\ntext</code>\nnot code", text);
        }

        [Test]
        public void isInlineCodeWork()
        {
            // Arrange
            lines = new string[] { "C `nano` other text", "text"};
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual("C <inline-code>nano</inline-code> other texttext", text);
        }

        [Test]
        public void isListItemWork()
        {
            // Arrange
            lines = new string[] { "text", "head", "----", "body", "text", "----",  };
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual("text<card-head id=1>head</card-head>\n<text id=1>body</text>\n<card-head id=2>text</card-head>\n<text id=2></text>\n", text);
        }

        [Test]
        public void isTextWork()
        {
            // Arrange
            lines = new string[] { "C nano other text", "", "", "text ", "text" };
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual("C nano other text<br>\ntext text", text);
        }


        // In Code block

        [Test]
        public void Code_InCodeBlock()
        {
            // Arrange
            lines = new string[] { "text",
                "\tcode start", "\t\tinline code", "text", "end"};
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual("text<code>code start\n\tinline code</code>\ntextend", text);
        }

        [Test]
        public void InlineCode_InCodeBlock()
        {
            // Arrange
            lines = new string[] { "text", 
                "\tcode start", "\t````inline code", "X `1` Y", "end"};
            // Act
            text = _disassemble.Disassemble(lines,0);
            // Assert
            Assert.AreEqual("text<code>code start\n````inline code</code>\nX <inline-code>1</inline-code> Yend", text);
        }

        [Test]
        public void ListItem_InCodeBlock()
        {
            // Arrange
            lines = new string[] { "\ttext",
                "\tcode start", "\t----", "\ttext", "end"};
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual("<code>text\ncode start\n----\ntext</code>\nend", text);
        }

        [Test]
        public void Text_InCodeBlock()
        {
            // Arrange
            lines = new string[] { "\ttext",
                "\tcode start", "\tW", "\ttext", "end"};
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual("<code>text\ncode start\nW\ntext</code>\nend", text);
        }


        // Nested lists

        [Test]
        public void ZeroNestingItemList()
        {
            // Arrange
            lines = new string[] { "head", "----", "body", "head1", "----", "body1", "head2", "----", "body2" };
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual("<card-head id=1>head</card-head>\n<text id=1>body</text>\n" +
                "<card-head id=2>head1</card-head>\n<text id=2>body1</text>\n" +
                "<card-head id=3>head2</card-head>\n<text id=3>body2</text>\n", text);
        }

        [Test]
        public void OneNestingItemList()
        {
            // Arrange
            lines = new string[] { "head", "----", "body", "\tsub-head" ,"\t----", "\tsub-body", "\tsub-head1" ,"\t----", "\tsub-body1", "\tsub-head2" ,"\t----", "\tsub-body2",
                "head1", "----", "body1", "\tsub-head" ,"\t----", "\tsub-body", "\tsub-head1" ,"\t----", "\tsub-body1", "\tsub-head2" ,"\t----", "\tsub-body2",
                "head2", "----", "body2", "\tsub-head" ,"\t----", "\tsub-body", "\tsub-head1" ,"\t----", "\tsub-body1", "\tsub-head2" ,"\t----", "\tsub-body2", };
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual(string.Format("<card-head id=4>head</card-head>\n<text id=4>body{0}</text>\n",
                "<card-head id=1>sub-head</card-head>\n<text id=1>sub-body</text>\n" +
                "<card-head id=2>sub-head1</card-head>\n<text id=2>sub-body1</text>\n"+
                "<card-head id=3>sub-head2</card-head>\n<text id=3>sub-body2</text>\n") +

                string.Format("<card-head id=8>head1</card-head>\n<text id=8>body1{0}</text>\n",
                "<card-head id=5>sub-head</card-head>\n<text id=5>sub-body</text>\n" +
                "<card-head id=6>sub-head1</card-head>\n<text id=6>sub-body1</text>\n" +
                "<card-head id=7>sub-head2</card-head>\n<text id=7>sub-body2</text>\n") +

                string.Format("<card-head id=12>head2</card-head>\n<text id=12>body2{0}</text>\n",
                "<card-head id=9>sub-head</card-head>\n<text id=9>sub-body</text>\n" +
                "<card-head id=10>sub-head1</card-head>\n<text id=10>sub-body1</text>\n" +
                "<card-head id=11>sub-head2</card-head>\n<text id=11>sub-body2</text>\n"), text);
        }

        [Test]
        public void TwoNestingItemList()
        {
            // Arrange
            lines = new string[] { "head", "----", "body", "\thead1", "\t----", "\tbody1", "\t\thead2", "\t\t----", "\t\tbody2" };
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual(string.Format("<card-head id=3>head</card-head>\n<text id=3>body{0}</text>\n",
                string.Format("<card-head id=2>head1</card-head>\n<text id=2>body1{0}</text>\n",
                "<card-head id=1>head2</card-head>\n<text id=1>body2</text>\n")) 
                , text);
        }

        // Combined tests

        [Test]
        public void CombinedTest_TwoListItem_OneCode_ThreeInlineCode_Text()
        {
            // Arrange
            lines = new string[] { "head", "----", "body", "\tcode", "\tls -ahl" , "text `wa` g",
                "head1", "----", "`body1`","text", "`inline_`" };
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual(string.Format("<card-head id=1>head</card-head>\n<text id=1>body{0}</text>\n",
                "<code>code\nls -ahl</code>\n" + "text <inline-code>wa</inline-code> g") +
                string.Format("<card-head id=2>head1</card-head>\n<text id=2><inline-code>body1</inline-code>{0}</text>\n",
                "text<inline-code>inline_</inline-code>")
                , text);
        }

    }
}
