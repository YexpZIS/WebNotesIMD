using NUnit.Framework;
using Parser;
using Parser.HtmlObjects;
using Parser.Tags;
using Microsoft.Extensions.DependencyInjection;
using Parser.Disassemble;
using Parser.Helpers;

namespace Tests.Parser
{
    class ListItem_Test
    {
        private ListItem _item;

        private string[] _lines;
        private string text;

        [OneTimeSetUp]
        public void Init()
        {
            ServiceCollection services = new ServiceCollection();

            // Disassemble
            services.AddTransient<IDisassemble, BodyDisassemble>();

            // Helpers
            services.AddSingleton<IDepth, Depth>();
            services.AddSingleton<Index>();
            services.AddSingleton<Seeker>();

            // HtmlObjects
            services.AddSingleton<ListItem>();
            services.AddSingleton<Text>();

            // Tags
            services.AddScoped<ITag, TestTags>();

            var provider = services.BuildServiceProvider();

            _item = provider.GetService<ListItem>();
        }

        [SetUp]
        public void ClearLocalVariables()
        {
            _lines = null;
            text = "";
        }

        [Test]
        public void SimpleTest()
        {
            // Arrange
            // ToDo Mock<BodyDisassemble> и протестировать метод GetBody
            _lines = new string[] { "head", "----", "body", "text" };

            /*Mock<IBodyDisassemble> mock = new Mock<IBodyDisassemble>();
            mock.Setup(x=>x.Disassemble(0)).Returns("body");
            _item.SetBodyDisassemble(mock.Object);*/

            //TestBody t = new TestBody();
            //_item.SetBodyDisassemble(t);

            // Act
            text = _item.isHtmlObject(_lines, 0);
            // Assert
            Assert.AreEqual(string.Format("<card-head id={0}>{1}</card-head>\n", 1, "head") +
                string.Format("<text id={0}>{1}</text>\n", 1, "body\ntext"), text);
        }
    }

    /*class TestBody : IBodyDisassemble
    {
        string body = "";

        public string Disassemble(int depth)
        {
            return body;
        }

        public void SetData(ref string[] BodyData, int id = 0)
        {
            body = string.Join("\n", BodyData);
        }
    }*/
}
