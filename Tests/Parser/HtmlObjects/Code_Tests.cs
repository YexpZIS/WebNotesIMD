using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Parser.Helpers;
using Parser.HtmlObjects;
using Parser.Tags;

namespace Tests.Parser.HtmlObjects
{
    class Code_Tests : IHtmlObject
    {
        private Code _code;

        [OneTimeSetUp]
        public void Init()
        {
            Create();
            services.AddScoped<Text>();
            services.AddScoped<Code>();
            Build();

            _code = provider.GetService<Code>();
        }

        [Test]
        public void SimpleTest()
        {
            // Arrange
            _lines = new string[] { "\t", "\tcode", "\t" };
            // Act
            text = _code.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual("<code>\ncode\n</code>\n", text);
            Assert.AreEqual(2, _code.GetIndex());
        }

        [Test]
        public void NoCode()
        {
            // Arrange
            _lines = new string[] { "\t", "code", "\t" };
            // Act
            text = _code.isHtmlObject(_lines, 1, 0);
            // Assert
            Assert.AreEqual(null, text);
            Assert.AreEqual(1, _code.GetIndex());
        }

        [Test]
        public void SpaceBetweenTwoBlocksOfCode()
        {
            // Arrange
            _lines = new string[] { "\techo 'text'", "\t", "\tls -ahl" ,"" ,"\trm -rf"};
            // Act
            text = _code.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual("<code>" +
                string.Join("\n", new string[] { "echo 'text'", "", "ls -ahl" }) + 
                "</code>\n", text);
            Assert.AreEqual(2, _code.GetIndex());
        }

        [Test]
        public void RemoveSomeTabs()
        {
            // Arrange
            _lines = new string[] { "\t\techo 'text'", "\t\t\t", "\tls -ahl", "", "\trm -rf" };
            // Act
            text = _code.isHtmlObject(_lines, 1, 0);
            // Assert
            Assert.AreEqual("<code>" +
                string.Join("\n", new string[] { "\t\t", "ls -ahl" }) +
                "</code>\n", text);
            Assert.AreEqual(2, _code.GetIndex());
        }

        [Test]
        public void FourSpacesInsteadOneTab()
        {
            // Arrange
            _lines = new string[] { "    echo 'text'", "        \t", "ls -ahl", "", "\trm -rf" };
            // Act
            text = _code.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual("<code>" +
                string.Join("\n", new string[] { "echo 'text'", "    \t" }) +
                "</code>\n", text);
            Assert.AreEqual(1, _code.GetIndex());
        }
    }
}
