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
    class FolderItem_Tests : IHtmlObject
    {
        private FolderItem _folder;

        [OneTimeSetUp]
        public void Init()
        {
            Create();
            services.AddSingleton<FolderItem>();
            Build();

            _folder = provider.GetService<FolderItem>();
        }

        /*[Test]
        public void SimpleTest()
        {
            // Arrange
            _lines = new string[] { "- Backup", "\t* [Text](path/file)", "\t* [Text2](path/file)",
                                    "- Title2", "\t* [Tex2](path/file)", "\t* [Text3](path/file)"};
            // Act
            text = _summary.isHtmlObject(_lines, 0, 0);
            // Assert
            Assert.AreEqual("", text);
        }*/
    }
}
