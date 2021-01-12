using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Parser.Disassemble;
using Parser.Helpers;
using Parser.TableOfContents;
using Parser.Tags;
using System.Collections.Generic;
using System.Text;

namespace Tests.Parser.Disassemble
{
    class SummaryDisassemble_Tests
    {
        private ServiceProvider _provider;
        private IDisassemble _disassemble;

        private string[] lines;
        private string text;

        [OneTimeSetUp]
        public void Init()
        {
            ServiceCollection services = new ServiceCollection();

            // Disassemble
            services.AddTransient<SummaryDisassemble>();

            // Helpers
            services.AddSingleton<IDepth, Depth>();
            services.AddSingleton<LineModifier>();
            services.AddSingleton<Index>();
            services.AddSingleton<Seeker>();

            // HtmlObjects
            services.AddTransient<FolderItem>();
            services.AddSingleton<BookTitle>();
            services.AddSingleton<ButtonToPage>();
            services.AddSingleton<TextPlug>();

            // Tags
            services.AddSingleton<ITag, TestTags>();

            _provider = services.BuildServiceProvider();
        }

        [SetUp]
        public void GetDisassemble()
        {
            _disassemble = _provider.GetService<SummaryDisassemble>();
            _provider.GetService<ITag>().ResetId();
        }

        // is Object Work

        [Test]
        public void IsBookTitleWork()
        {
            // Arrange
            lines = new string[] { "",  "# text" };
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual("<bookName>text</bookName>", text);
        }

        [Test]
        public void IsButtonToPageWork()
        {
            // Arrange
            lines = new string[] { "* [PageX0](path)" };
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual("<button url={2}/path>PageX0</button>", text);
        }

        [Test]
        public void IsFolderItemWork()
        {
            // Arrange
            lines = new string[] { "- MyFolder" };
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual("<folder id=1>MyFolder<buttons></buttons></folder>", text);
        }

        [Test]
        public void IsTextPlugWork()
        {
            // Arrange
            lines = new string[] {"text","text" };
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual("", text);
        }


        [Test]
        public void SimpleTest()
        {
            // Arrange
            lines = new string[] {"# Book Nemo" , "* [README.md](path.md)",
            "- Folder", "\t* [Page1](folder/p1.md)", "\t* [Page1](folder/p1.md)", 
            "- Folder2"};

            // Act
            text = _disassemble.Disassemble(lines, 0);

            // Assert
            Assert.AreEqual("<bookName>Book Nemo</bookName>"+
                "<button url={2}/path.md>README.md</button>" +
                string.Format("<folder id={0}>{1}<buttons>{2}</buttons></folder>",
                1, "Folder", "<button url={2}/folder/p1.md>Page1</button>" + "<button url={2}/folder/p1.md>Page1</button>")
                + string.Format("<folder id={0}>{1}<buttons>{2}</buttons></folder>",
                2, "Folder2", ""), text);
        }

        [Test]
        public void NoSummary()
        {
            // Arrange
            lines = new string[] { "","","","","" };
            // Act
            text = _disassemble.Disassemble(lines, 0);
            // Assert
            Assert.AreEqual("", text);
        }

    }
}
