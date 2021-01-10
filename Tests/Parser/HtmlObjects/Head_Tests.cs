using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Parser.HtmlObjects;

namespace Tests.Parser.HtmlObjects
{
    class Head_Tests : IHtmlObject
    {
        private Head _head;

        [OneTimeSetUp]
        public void Init()
        {
            Create();
            services.AddSingleton<Head>();
            Build();

            _head = provider.GetService<Head>();
        }

        [Test]
        public void SimpleTest()
        {
            // Arrange
            _lines = new string[] { "Title","====","tag1, tag2", "Head" ,"----", "Body"};
            // Act
            text = _head.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual("<head>Title</head>\n<tags>tag1, tag2</tags>\n", text);
            Assert.AreEqual(2, _head.GetIndex());
        }

        [Test]
        public void LargeHeaderAndManyTags()
        {
            // Arrange
            _lines = new string[] { "Title", "T", "T2", "T3", "====", "tag1, tag2","tag","tag","unT", "Head", "----", "Body" };
            // Act
            text = _head.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual("<head>Title<br>\nT<br>\nT2<br>\nT3</head>\n<tags>tag1, tag2<br>\ntag<br>\ntag<br>\nunT</tags>\n", text);
            Assert.AreEqual(8, _head.GetIndex());
        }

        [Test]
        public void SetFirstIndex()
        {
            // Arrange
            _lines = new string[] { "Title", "T", "T2", "T3", "====", "tag1, tag2", "tag", "tag", "unT", "Head", "----", "Body" };
            // Act
            text = _head.isHtmlObject(_lines, 1, 0);
            // Assert
            Assert.AreEqual(null, text);
            Assert.AreEqual(1, _head.GetIndex());
        }

        [Test]
        public void NoTitleAndTags()
        {
            // Arrange
            _lines = new string[] { "Title", "T", "T2", "T3", "r", "tag1, tag2", "tag", "tag", "unT", "Head", "----", "Body" };
            // Act
            text = _head.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual(null, text);
            Assert.AreEqual(0, _head.GetIndex());
        }
    }
}
