using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Parser.Helpers;
using Parser.HtmlObjects;
using Parser.Tags;

namespace Tests.Parser.HtmlObjects
{
    class Code_Tests
    {
        private Code _code;

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
            services.AddScoped<Text>();
            services.AddScoped<Code>();

            var provider = services.BuildServiceProvider();

            _code = provider.GetService<Code>();
        }

        [Test]
        public void SimpleTest()
        {
            // Arrange
            lines = new string[] { "\t", "\tcode", "\t" };
            // Act
            text = _code.isHtmlObject(lines, 0, 0);
            // Assert
            Assert.AreEqual("<code>\ncode\n</code>\n", text);
            Assert.AreEqual(2, _code.GetIndex());
        }

        [Test]
        public void NoCode()
        {
            // Arrange
            lines = new string[] { "\t", "code", "\t" };
            // Act
            text = _code.isHtmlObject(lines, 1, 0);
            // Assert
            Assert.AreEqual(null, text);
            Assert.AreEqual(1, _code.GetIndex());
        }

        [Test]
        public void SpaceBetweenTwoBlocksOfCode()
        {
            // Arrange
            lines = new string[] { "\techo 'text'", "\t", "\tls -ahl" ,"" ,"\trm -rf"};
            // Act
            text = _code.isHtmlObject(lines, 0, 0);
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
            lines = new string[] { "\t\techo 'text'", "\t\t\t", "\tls -ahl", "", "\trm -rf" };
            // Act
            text = _code.isHtmlObject(lines, 1, 0);
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
            lines = new string[] { "    echo 'text'", "        \t", "ls -ahl", "", "\trm -rf" };
            // Act
            text = _code.isHtmlObject(lines, 0, 0);
            // Assert
            Assert.AreEqual("<code>" +
                string.Join("\n", new string[] { "echo 'text'", "    \t" }) +
                "</code>\n", text);
            Assert.AreEqual(1, _code.GetIndex());
        }
    }
}
