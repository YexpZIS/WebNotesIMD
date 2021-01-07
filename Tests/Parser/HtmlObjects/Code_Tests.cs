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
            Assert.AreEqual("<code><br>\ncode<br>\n<br>\n</code>\n", text);
        }
    }
}
