using Parser.HtmlObjects;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Parser.Disassemble
{
    public class BodyDisassemble : AbsDisassemble
    {
        public BodyDisassemble(IServiceProvider provider) : base(provider)
        {

        }

        protected override void InitHtmlObjects()
        {
            htmlObjects.Clear();
            htmlObjects.Add(_provider.GetService<Head>());
            htmlObjects.Add(_provider.GetService<ListItem>());
            htmlObjects.Add(_provider.GetService<Code>());
            htmlObjects.Add(_provider.GetService<InlineCode>());
            htmlObjects.Add(_provider.GetService<Text>());
        }

        
    }
}
