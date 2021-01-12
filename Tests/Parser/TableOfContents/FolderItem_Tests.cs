using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Parser.TableOfContents;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Parser.HtmlObjects;
using Moq;
using Parser.Disassemble;
using Parser.Tags;
using Parser.Helpers;

namespace Tests.Parser.TableOfContents
{
    class FolderItem_Tests : IHtmlObject
    {
        private FolderItem _folder;

        private ITag _tag;
        private Seeker _seeker;

        [OneTimeSetUp]
        public void Init()
        {
            Create();
            services.AddTransient<FolderItem>();
            services.AddTransient<SummaryDisassemble>();
            services.AddSingleton<BookTitle>();
            services.AddSingleton<ButtonToPage>();
            services.AddSingleton<TextPlug>();
            Build();

            _tag = provider.GetService<ITag>();
            _seeker = provider.GetService<Seeker>();
        }

        [SetUp]
        public void ClearLocalValues()
        {
            _folder = null;
            _tag.ResetId();
        }

        [Test]
        public void SimpleTest()
        {
            // Arrange
            _lines = new string[] { "- Backup", "\t* [Text](path/file)", "\t* [Text](path/file)",
                                    "- Title2", "\t* [Tex2](path/file)", "\t* [Text3](path/file)"};
            Mock<IDisassemble> mock = new Mock<IDisassemble>();
            mock.Setup(x => x.Disassemble(new string[] { "\t* [Text](path/file)", "\t* [Text](path/file)" }, 1)).Returns("Body OK");
            
            _folder = new FolderItem(_tag, _seeker, null);
            _folder.SetDisassembler(mock.Object);

            // Act
            text = _folder.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual("<folder id=1>Backup<buttons>Body OK</buttons></folder>", text);
        }

        [Test]
        public void OnZeroDepthWithoutButtons()
        {
            // Arrange
            _lines = new string[] { "- Backup", "* [Text](path/file)", "* [Text](path/file)"};
            Mock<IDisassemble> mock = new Mock<IDisassemble>();
            mock.Setup(x => x.Disassemble(new string[] { }, 1)).Returns("Body OK");

            _folder = new FolderItem(_tag, _seeker, null);
            _folder.SetDisassembler(mock.Object);

            // Act
            text = _folder.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual("<folder id=1>Backup<buttons>Body OK</buttons></folder>", text);
        }

        [Test]
        public void FolderHasNotGotButtons()
        {
            // Arrange
            _lines = new string[] { "- Backup", 
                                    "- Title2", "\t* [Tex2](path/file)", "\t* [Text3](path/file)"};

            Mock<IDisassemble> mock = new Mock<IDisassemble>();
            mock.Setup(x => x.Disassemble(null, 0)).Returns("Body OK");

            _folder = new FolderItem(_tag, _seeker, null);
            _folder.SetDisassembler(mock.Object);

            // Act
            text = _folder.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual("<folder id=1>Backup<buttons></buttons></folder>", text);
        }




        // Integrated Tests

        [Test]
        public void Simple_IntegratedTest()
        {
            // Arrange
            _lines = new string[] { "- Title2", "\t* [Tex2](path/file)", "\t* [Text3](path/file)"};

            _folder = provider.GetService<FolderItem>();

            // Act
            text = _folder.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual(string.Format("<folder id=1>Title2<buttons>{0}</buttons></folder>",
                "<button url={2}/path/file>Tex2</button>" + "<button url={2}/path/file>Text3</button>"), text);
        }


        [Test]
        public void FolderInFolder_IntegratedTest()
        {
            // Arrange
            _lines = new string[] { "- Backup", "\t- F2", "\t\t* [F2](path/file)", "\t* [Tex2](path/file)",
                                    "- Title2", "\t* [Tex2](path/file)", "\t* [Text3](path/file)"};

            _folder = provider.GetService<FolderItem>();
            
            // Act
            text = _folder.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual(string.Format("<folder id=1>Backup<buttons>{0}</buttons></folder>" ,
                "<folder id=2>F2<buttons><button url={2}/path/file>F2</button></buttons></folder>" + "<button url={2}/path/file>Tex2</button>"), text);
        }
    }
}
