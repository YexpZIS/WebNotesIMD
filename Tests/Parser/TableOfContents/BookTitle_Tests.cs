using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Tests.Parser.HtmlObjects;
using Parser.TableOfContents;

namespace Tests.Parser.TableOfContents
{
    class BookTitle_Tests : IHtmlObject
    {
        private BookTitle _title;

        [OneTimeSetUp]
        public void Init()
        {
            Create();
            services.AddSingleton<BookTitle>();
            Build();

            _title = provider.GetService<BookTitle>();
        }

        [Test]
        public void DetectBookTitle_WhenItOn_ZeroLine()
        {
            // Arrange
            _lines = new string[] { "# Title", "" };
            // Act
            text = _title.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual("<bookName>Title</bookName>", text);
        }

        [Test]
        public void DetectBookTitle_WhenItOn_FirstLine()
        {
            // Arrange
            _lines = new string[] { "", "# Title", "" };
            // Act
            text = _title.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual("<bookName>Title</bookName>", text);
        }

        [Test]
        public void DetectBookTitle_WhenItOn_TenLine()
        {
            // Arrange
            _lines = new string[] { "1","2","3","4","5","6","7","8","9", "# More than book", "" };
            // Act
            text = _title.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual("<bookName>More than book</bookName>", text);
        }

        [Test]
        public void NoBookTitle()
        {
            // Arrange
            _lines = new string[] { "", "" };
            // Act
            text = _title.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual(null, text);
        }

        
    }
}
