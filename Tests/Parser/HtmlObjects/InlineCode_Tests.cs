using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Parser.Helpers;
using Parser.HtmlObjects;
using Parser.Tags;

namespace Tests.Parser.HtmlObjects
{
    class InlineCode_Tests : IHtmlObject
    {
        private InlineCode _inlineCode;

        [OneTimeSetUp]
        public void Init()
        {
            Create();
            services.AddSingleton<InlineCode>();
            Build();

            _inlineCode = provider.GetService<InlineCode>();
        }

        [Test]
        public void SimpleTest()
        {
            // Arrange
            _lines = new string[] { "`text` other text"};
            // Act
            text = _inlineCode.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual("<inline-code>text</inline-code> other text", text);
            Assert.AreEqual(0, _inlineCode.GetIndex());
        }

        [Test]
        public void TwoInlineCodeInOneLine()
        {
            // Arrange
            _lines = new string[] { "`text` other text `x`" };
            // Act
            text = _inlineCode.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual("<inline-code>text</inline-code> other text <inline-code>x</inline-code>", text);
            Assert.AreEqual(0, _inlineCode.GetIndex());
        }

        [Test]
        public void WhenInlineCodeAreNotClosed()
        {
            // Arrange
            _lines = new string[] { "`text other text " };
            // Act
            text = _inlineCode.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual("<inline-code>text other text </inline-code>", text);
            Assert.AreEqual(0, _inlineCode.GetIndex());
        }

        [Test]
        public void NoInlineCode()
        {
            // Arrange
            _lines = new string[] { "text other text " };
            // Act
            text = _inlineCode.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual(null, text);
            Assert.AreEqual(0, _inlineCode.GetIndex());
        }
    }
}
