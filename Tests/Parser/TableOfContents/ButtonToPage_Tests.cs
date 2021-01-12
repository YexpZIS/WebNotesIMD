using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Parser.TableOfContents;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Parser.HtmlObjects;
using Moq;

namespace Tests.Parser.TableOfContents
{
    class ButtonToPage_Tests : IHtmlObject
    {
        private ButtonToPage _button;

        [OneTimeSetUp]
        public void Init()
        {
            Create();
            services.AddSingleton<ButtonToPage>();
            Build();

            _button = provider.GetService<ButtonToPage>();
        }

        [Test]
        public void SimpleTest()
        {
            // Arrange
            _lines = new string[] {"* [Text](path/to/file)" };
            // Act
            text = _button.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual("<button url={2}/path/to/file>Text</button>", text);
        }

        [Test]
        public void Depth_One()
        {
            // Arrange
            _lines = new string[] { "\t* [Text](path/to/file)" };
            // Act
            text = _button.isHtmlObject(_lines, 0, 1);
            // Assert
            Assert.AreEqual("<button url={2}/path/to/file>Text</button>", text);
        }

        [Test]
        public void DepthOne()
        {
            // Arrange
            _lines = new string[] { "\t* [Text](path/to/file)"};
            // Act
            text = _button.isHtmlObject(_lines, 0, 1);
            // Assert
            Assert.AreEqual("<button url={2}/path/to/file>Text</button>", text);
        }

        [Test]
        public void NoButtons()
        {
            // Arrange
            _lines = new string[] { "\tdasklf jansg4ol" };
            // Act
            text = _button.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual(null, text);
        }

        [Test]
        public void NotOnlyButton()
        {
            // Arrange
            _lines = new string[] { "* [Title](path)some txt"};
            // Act
            text = _button.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual("<button url={2}/path>Title</button>", text);
        }

        [Test]
        public void DetectTwoButtons()
        {
            // Arrange
            _lines = new string[] { "* [Text](path/to/file)", "* [Text2](path/to/file2)" };
            // Act
            text = _button.isHtmlObject(_lines, 0, 0);
            text += _button.isHtmlObject(_lines, 1, 0);
            // Assert
            Assert.AreEqual("<button url={2}/path/to/file>Text</button><button url={2}/path/to/file2>Text2</button>", text);
        }
    }
}
