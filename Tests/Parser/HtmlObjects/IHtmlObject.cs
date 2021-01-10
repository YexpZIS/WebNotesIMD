using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Parser.Helpers;
using Parser.Tags;

namespace Tests.Parser.HtmlObjects
{
    public abstract class IHtmlObject
    {
        protected ServiceProvider provider { get; set; }
        protected ServiceCollection services { get; set; }

        protected string[] _lines;
        protected string text;

        protected void Create()
        {
            services = new ServiceCollection();
            services.AddSingleton<IDepth, Depth>();
            services.AddSingleton<Index>();
            services.AddSingleton<LineModifier>();
            services.AddSingleton<Seeker>();
            services.AddSingleton<ITag, TestTags>();
        }
        protected void Build()
        {
            provider = services.BuildServiceProvider();
        }

        [SetUp]
        protected void ClearValues()
        {
            _lines = null;
            text = null;
        }
    }
}
