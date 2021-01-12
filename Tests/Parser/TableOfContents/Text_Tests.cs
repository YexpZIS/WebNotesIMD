using System;
using System.Collections.Generic;
using System.Text;
using Tests.Parser.HtmlObjects;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Parser.TableOfContents;

namespace Tests.Parser.TableOfContents
{
    class Text_Tests : IHtmlObject
    {
        private TextPlug _text;

        [OneTimeSetUp]
        public void Init()
        {
            Create();
            services.AddSingleton<TextPlug>();
            Build();

            _text = provider.GetService<TextPlug>();
        }

        // Arrange
        [TestCase("")]
        [TestCase(null)]
        [TestCase("Cup of code")]
        public void SimpleTest(string text)
        {
            // Act
            this.text = _text.isHtmlObject(new string[] { text}, 0, 0);
            //Assert
            Assert.AreEqual(string.Empty, this.text);
        }
    }
}
