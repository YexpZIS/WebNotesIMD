using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Parser.Helpers;
using Parser.HtmlObjects;
using Parser.Tags;

namespace Tests.Parser.HtmlObjects
{
    class InlineCode_Tests
    {
        private InlineCode _inlineCode;

        private string[] lines;
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
            services.AddSingleton<InlineCode>();

            var provider = services.BuildServiceProvider();

            _inlineCode = provider.GetService<InlineCode>();
        }

        [Test]
        public void SimpleTest()
        {
            // Arrange
            lines = new string[] { "````text```` other text"};
            // Act
            text = _inlineCode.isHtmlObject(lines, 0, 0);
            // Assert
            Assert.AreEqual("<inline-code>text</inline-code> other text", text);
        }

        [Test]
        public void TwoInlineCodeInOneLine()
        {
            // Arrange
            lines = new string[] { "````text```` other text ````x````" };
            // Act
            text = _inlineCode.isHtmlObject(lines, 0, 0);
            // Assert
            Assert.AreEqual("<inline-code>text</inline-code> other text <inline-code>x</inline-code>", text);
        }

        [Test]
        public void WhenInlineCodeAreNotClosed()
        {
            // Arrange
            lines = new string[] { "````text other text " };
            // Act
            text = _inlineCode.isHtmlObject(lines, 0, 0);
            // Assert
            Assert.AreEqual("<inline-code>text other text </inline-code>", text);
        }
    }
}
