using NUnit.Framework;
using Parser;
using Parser.HtmlObjects;
using Parser.Tags;
using Microsoft.Extensions.DependencyInjection;
using Parser.Disassemble;
using Parser.Helpers;
using Moq;

namespace Tests.Parser.HtmlObjects
{
    class ListItem_Test
    {
        private ListItem _item;

        private ITag _tag;
        private Seeker _seeker;

        private string[] _lines;
        private string text;

        [OneTimeSetUp]
        public void Init()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IDepth, Depth>();
            services.AddSingleton<Index>();
            services.AddSingleton<LineModifier>();
            services.AddSingleton<Seeker>();
            services.AddSingleton<ITag, TestTags>();

            var provider = services.BuildServiceProvider();

            _tag = provider.GetService<ITag>();
            _seeker = provider.GetService<Seeker>();
            

            _item = new ListItem(null,_tag,_seeker);
        }

        [SetUp]
        public void ClearLocalVariables()
        {
            _tag.id = 0;
            _lines = null;
            text = "";
        }

        [Test]
        public void SimpleTest()
        {
            // Arrange
            _lines = new string[] { "head", "----", "body", "text" };

            Mock<IDisassemble> mock = new Mock<IDisassemble>();
            mock.Setup(x=>x.Disassemble(new string[] { "body", "text" },0)).Returns("body\ntext");
            _item = new ListItem(mock.Object, _tag, _seeker);
            // Act
            text = _item.isHtmlObject(_lines, 0, -1);
            // Assert
            Assert.AreEqual(string.Format("<card-head id={0}>{1}</card-head>\n", 1, "head") +
                string.Format("<text id={0}>{1}</text>\n", 1, "body\ntext"), text);
            Assert.AreEqual(3, _item.GetIndex());
        }



        [Test]
        public void WhenBetweenItems_NoLine()
        {
            // Arrange
            _lines = new string[] { "head", "----",  "text", "----", "some text" };

            Mock<IDisassemble> mock = new Mock<IDisassemble>();
            mock.Setup(x => x.Disassemble(new string[] {  "text" }, 0)).Returns("body");
            _item = new ListItem(mock.Object, _tag, _seeker);

            // Act
            text = _item.isHtmlObject(_lines, 0, -1);

            // Assert
            Assert.AreEqual(string.Format("<card-head id={0}>{1}</card-head>\n", 1, "head") +
                string.Format("<text id={0}>{1}</text>\n", 1, ""), text);
            Assert.AreEqual(1, _item.GetIndex());
        }
        [Test]
        public void WhenBetweenItems_OneLine()
        {
            // Arrange
            _lines = new string[] { "head", "----", "some text", "text","----","some text" };

            Mock<IDisassemble> mock = new Mock<IDisassemble>();
            mock.Setup(x => x.Disassemble(new string[] { "some text" }, 0)).Returns("some text");
            _item = new ListItem(mock.Object, _tag, _seeker);

            // Act
            text = _item.isHtmlObject(_lines, 0, -1);

            // Assert
            Assert.AreEqual(string.Format("<card-head id={0}>{1}</card-head>\n", 1, "head") +
                string.Format("<text id={0}>{1}</text>\n", 1, "some text"), text);
            Assert.AreEqual(2, _item.GetIndex());
        }
        
        [Test]
        public void WhenBetweenItems_ManyLine()
        {
            // Arrange
            var result = string.Join("\n", new string[] { "body", "random text", "random text0", "random text5" });

            _lines = new string[] { "head", "----", "body", "random text", "random text0", "random text5", "text", "----", "some text" };

            Mock<IDisassemble> mock = new Mock<IDisassemble>();
            mock.Setup(x => x.Disassemble(new string[] { "body", "random text", "random text0", "random text5" }, 0)).Returns(result);
            _item = new ListItem(mock.Object, _tag, _seeker);

            // Act
            text = _item.isHtmlObject(_lines, 0, -1);

            // Assert
            Assert.AreEqual(string.Format("<card-head id={0}>{1}</card-head>\n", 1, "head") +
                string.Format("<text id={0}>{1}</text>\n", 1, result), text);
            Assert.AreEqual(5, _item.GetIndex());
        }


        // Depth tests
        // Zero
        [Test]
        public void WhenBetweenItemsToEnd_OnZeroDepth()
        {
            // Arrange
            _lines = new string[] { "head", "----", "\tbody", "\ttext", "\t----", "\tsome text" };

            Mock<IDisassemble> mock = new Mock<IDisassemble>();
            mock.Setup(x => x.Disassemble(new string[] { "\tbody", "\ttext", "\t----", "\tsome text" }, 0)).Returns("body OK");
            _item = new ListItem(mock.Object, _tag, _seeker);

            // Act
            text = _item.isHtmlObject(_lines, 0, -1);

            // Assert
            Assert.AreEqual(string.Format("<card-head id={0}>{1}</card-head>\n", 1, "head") +
                string.Format("<text id={0}>{1}</text>\n", 1, "body OK"), text);
            Assert.AreEqual(5, _item.GetIndex());
        }

        [Test]
        public void WhenBetweenItems_OnZeroDepth()
        {
            // Arrange
            _lines = new string[] { "head", "----", "\tbody", "\ttext", "\t----", "\tsome text", "head2", "----", "body" };

            Mock<IDisassemble> mock = new Mock<IDisassemble>();
            mock.Setup(x => x.Disassemble(new string[] { "\tbody", "\ttext", "\t----", "\tsome text" }, 0)).Returns("body OK");
            _item = new ListItem(mock.Object, _tag, _seeker);

            // Act
            text = _item.isHtmlObject(_lines, 0, -1);

            // Assert
            Assert.AreEqual(string.Format("<card-head id={0}>{1}</card-head>\n", 1, "head") +
                string.Format("<text id={0}>{1}</text>\n", 1, "body OK"), text);
            Assert.AreEqual(5, _item.GetIndex());
        }

        // First

        [Test]
        public void WhenBetweenItemsToEnd_OnFirstDepth()
        {
            // Arrange
            _lines = new string[] { "head", "----", "\tbody", "\ttext", "\t----", "\tsome text" };

            Mock<IDisassemble> mock = new Mock<IDisassemble>();
            mock.Setup(x => x.Disassemble(new string[] { "\tsome text" }, 1)).Returns("body OK");
            _item = new ListItem(mock.Object, _tag, _seeker);

            // Act
            text = _item.isHtmlObject(_lines, 3, -1);

            // Assert
            Assert.AreEqual(string.Format("<card-head id={0}>{1}</card-head>\n", 1, "text") +
                string.Format("<text id={0}>{1}</text>\n", 1, "body OK"), text);
            Assert.AreEqual(5, _item.GetIndex());
        }

        [Test]
        public void WhenBetweenItems_OnFirstDepth()
        {
            // Arrange
            _lines = new string[] { "head", "----", "\tbody", "\ttext", "\t----", "\tsome text", "\thead2", "\t----", "\tbody" };

            Mock<IDisassemble> mock = new Mock<IDisassemble>();
            mock.Setup(x => x.Disassemble(new string[] {  "\tsome text" }, 1)).Returns("body OK");
            _item = new ListItem(mock.Object, _tag, _seeker);

            // Act
            text = _item.isHtmlObject(_lines, 3, -1);

            // Assert
            Assert.AreEqual(string.Format("<card-head id={0}>{1}</card-head>\n", 1, "text") +
                string.Format("<text id={0}>{1}</text>\n", 1, "body OK"), text);
            Assert.AreEqual(5, _item.GetIndex());
        }



        [Test]
        public void BlockHasNotHead()
        {
            // Arrange
            _lines = new string[] {  "----", "body", "text" };

            Mock<IDisassemble> mock = new Mock<IDisassemble>();
            mock.Setup(x => x.Disassemble(new string[] { "body", "text" }, 0)).Returns("body\ntext");
            _item = new ListItem(mock.Object, _tag, _seeker);
            // Act
            text = _item.isHtmlObject(_lines, 0, -1);
            // Assert
            Assert.AreEqual(null, text);
            Assert.AreEqual(0, _item.GetIndex());
        }

        [Test]
        public void BlockHasNotBody()
        {
            // Arrange
            _lines = new string[] { "head", "----",  };

            Mock<IDisassemble> mock = new Mock<IDisassemble>();
            //mock.Setup(x => x.Disassemble(new string[] { "body", "text" }, 0)).Returns("body\ntext");
            _item = new ListItem(mock.Object, _tag, _seeker);
            // Act
            text = _item.isHtmlObject(_lines, 0, -1);
            // Assert
            Assert.AreEqual(string.Format("<card-head id={0}>{1}</card-head>\n", 1, "head") +
                string.Format("<text id={0}>{1}</text>\n", 1, ""), text);
            Assert.AreEqual(1, _item.GetIndex());
        }


        [Test]
        public void isValuesAreCleanOnSecondUse()
        {
            // Arrange
            _lines = new string[] { "head", "----", "\tbody", "\ttext", "\t----", "\tsome text", "\thead2", "\t----", "\tbody" };
            string[] _lines_2 = new string[] { "SHey", "----", "YoW", "\ttext", "\t----", "\tsome text", "\thead2", "\t----", "\tbody" };

            Mock<IDisassemble> mock = new Mock<IDisassemble>();
            mock.Setup(x => x.Disassemble(new string[] { "\tsome text" }, 1)).Returns("body OK");
            mock.Setup(x => x.Disassemble(new string[] { "YoW", "\ttext", "\t----", "\tsome text", "\thead2", "\t----", "\tbody" }, 0)).Returns("body OK 2");
            _item = new ListItem(mock.Object, _tag, _seeker);

            text = _item.isHtmlObject(_lines, 3, -1);
            text = _item.isHtmlObject(_lines_2, 0, 1);
            // Act

            // Assert
            Assert.AreEqual(string.Format("<card-head id={0}>{1}</card-head>\n", 2, "SHey") +
                string.Format("<text id={0}>{1}</text>\n", 2, "body OK 2"), text);
            Assert.AreEqual(8, _item.GetIndex());
        }

        [Test]
        public void isValuesAreCleanOnSecondUse_WhenHasNotBody()
        {
            // Arrange
            _lines = new string[] { "head", "----", "\tbody", "\ttext", "\t----", "\tsome text", "\thead2", "\t----", "\tbody" };
            string[] _lines_2 = new string[] { "SHey", "----" };

            Mock<IDisassemble> mock = new Mock<IDisassemble>();
            mock.Setup(x => x.Disassemble(new string[] { "\tsome text" }, 1)).Returns("body OK");
            mock.Setup(x => x.Disassemble(new string[] {  }, 0)).Returns("body OK 2");
            _item = new ListItem(mock.Object, _tag, _seeker);

            text = _item.isHtmlObject(_lines, 3, -1);
            text = _item.isHtmlObject(_lines_2, 0, 1);
            // Act

            // Assert
            Assert.AreEqual(string.Format("<card-head id={0}>{1}</card-head>\n", 2, "SHey") +
                string.Format("<text id={0}>{1}</text>\n", 2, "body OK 2"), text);
            Assert.AreEqual(1, _item.GetIndex());
        }
    }

    
}
