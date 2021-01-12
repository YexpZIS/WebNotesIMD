using Parser.Disassemble;
using Parser.Helpers;
using Parser.HtmlObjects;
using Parser.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Parser.TableOfContents;

namespace Parser.Disassemble
{
    public class SummaryDisassemble : AbsDisassemble
    {
        public SummaryDisassemble(IServiceProvider provider) : base(provider)
        {

        }

        protected override void InitHtmlObjects()
        {
            htmlObjects.Clear();
            htmlObjects.Add(_provider.GetService<BookTitle>());
            htmlObjects.Add(_provider.GetService<FolderItem>());
            htmlObjects.Add(_provider.GetService<ButtonToPage>());
            htmlObjects.Add(_provider.GetService<TableOfContents.TextPlug>());
        }
    }
}
