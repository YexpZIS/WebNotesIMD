using NUnit.Framework;
using Parser.HtmlObjects;
using Parser.Tags;
using Microsoft.Extensions.DependencyInjection;
using Parser.Helpers;

namespace Tests.Parser.HtmlObjects
{
    class Text_Tests : IHtmlObject
    {
        private Text _text;

        [OneTimeSetUp]
        public void Init()
        {
            Create();
            services.AddSingleton<Text>();
            Build();

            _text = provider.GetService<Text>();
        }


        [Test]
        public void SimpleTest()
        {
            // Arrange
            _lines = new string[] { "text" };
            // Act
            text = _text.isHtmlObject(_lines, 0, -1);
            // Assert
            Assert.AreEqual("text", text);
            Assert.AreEqual(0, _text.GetIndex());
        }


        [Test]
        public void RemoveTabsAndSpases_OnFirstDepth()
        {
            // Arrange
            _lines = new string[] { "\ttext" };
            // Act
            text = _text.isHtmlObject(_lines, 0, 1);
            // Assert
            Assert.AreEqual("text", text);
            Assert.AreEqual(0, _text.GetIndex());
        }

        [Test]
        public void RemoveTabs_OnThirdDepth()
        {
            // Arrange
            _lines = new string[] { "\t\t\t\ttext" };
            // Act
            text = _text.isHtmlObject(_lines, 0, 3);
            // Assert
            Assert.AreEqual("\ttext", text);
            Assert.AreEqual(0, _text.GetIndex());
        }

        [Test]
        public void RemoveSpases_OnThirdDepth()
        {
            // Arrange
            _lines = new string[] { "             text" };
            // Act
            text = _text.isHtmlObject(_lines, 0, 3);
            // Assert
            Assert.AreEqual(" text", text);
            Assert.AreEqual(0, _text.GetIndex());
        }

        [Test]
        public void RemoveTabsAndSpases_OnThirdDepth()
        {
            // Arrange
            _lines = new string[] { "\t    \t\t\ttext" };
            // Act
            text = _text.isHtmlObject(_lines, 0, 3);
            // Assert
            Assert.AreEqual("\t\ttext", text);
            Assert.AreEqual(0, _text.GetIndex());
        }

        [Test]
        public void ReturnDelimiterWhenStringEmpty()
        {
            // Arrange
            _lines = new string[] { "" };
            // Act
            text = _text.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual("", text);
        }

        [Test]
        public void OneSeparatorBetweenText()
        {
            // Arrange 
            _lines = new string[] { "text", "", "text" };

            // Act
            for (int i = 0; i < _lines.Length; i++)
            {
                text += _text.isHtmlObject(_lines, i, 0);
            }

            // Assert
            Assert.AreEqual("text<br>\ntext", text);
        }

        [Test]
        public void TwoSeparatorBetweenText()
        {
            // Arrange 
            _lines = new string[] {"text", "", "", "text" };

            // Act
            for (int i = 0; i < _lines.Length; i++) 
            {
                text += _text.isHtmlObject(_lines, i, 0);
                i = _text.GetIndex();
            }

            // Assert
            Assert.AreEqual("text<br>\n<br>\ntext", text);
        }

        [Test]
        public void SeparatorsInText()
        {
            // Arrange 
            _lines = new string[] { "","text", "", "", "text", "", "text","" };

            // Act
            for (int i = 0; i < _lines.Length; i++)
            {
                text += _text.isHtmlObject(_lines, i, 0);
                i = _text.GetIndex();
            }

            // Assert
            Assert.AreEqual("text<br>\n<br>\ntext<br>\ntext", text);
        }
    }
}
