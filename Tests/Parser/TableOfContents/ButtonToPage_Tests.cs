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
    class ButtonToPage_Tests : IHtmlObject
    {
        private ButtonToPage _button;

        [OneTimeSetUp]
        public void Init()
        {
            Create();
            services.AddSingleton<ButtonToPage>();
            Build();

            _button = provider.GetService<ButtonToPage>();
        }
    }
}
