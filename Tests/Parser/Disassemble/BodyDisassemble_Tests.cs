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
            services.AddScoped<ITag, TestTags>();

            _provider = services.BuildServiceProvider();
        }

        [SetUp]
        public void GetDisassemble()
        {
            _disassemble = _provider.GetService<IDisassemble>();
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
            Assert.AreEqual("<code>nano other text<br>\ntext<br>\n</code>\nnot code<br>\n", text);
        }

        [Test]
        public void isInlineCodeWork()
        {
            // Arrange
            lines = new string[] { "C ````nano```` other text", "text"};
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual("C <inline-code>nano</inline-code> other texttext<br>\n", text);
        }

        [Test]
        public void isListItemWork()
        {
            // Arrange
            lines = new string[] { "text", "head", "----", "body", "text", "----",  };
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual("text<br>\n<card-head id=1>head</card-head>\n<text id=1>body<br>\n</text>\n<card-head id=2>text</card-head>\n<text id=2></text>\n", text);
        }

        [Test]
        public void isTextWork()
        {
            // Arrange
            lines = new string[] { "C nano other text", "text" };
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual("C nano other text<br>\ntext<br>\n", text);
        }


        // In Code block

        [Test]
        public void InlineCode_InCodeBlock()
        {
            // Arrange
            lines = new string[] { "text", 
                "\tcode start", "\t````inline code", "X ````1```` Y", "end"};
            // Act
            text = _disassemble.Disassemble(lines,0);
            // Assert
            Assert.AreEqual("text<br>\n<code>code start<br>\n````inline code<br>\n</code>\nX <inline-code>1</inline-code> Yend<br>\n", text);
        }
    }
}
