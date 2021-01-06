using NUnit.Framework;
using Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Parser
{
    class ListItem_Test
    {
        private TestTags tags;
        private ListItem _item;

        private string[] _lines;
        private string text;

        [OneTimeSetUp]
        public void Init()
        {
            tags = new Test_Tags();
            _item = new ListItem(ref tags.tags, 0);
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

            TestBody t = new TestBody();
            _item.SetBodyDisassemble(t);

            // Act
            text = _item.isHtmlObject(ref _lines, 0);
            // Assert
            Assert.AreEqual(string.Format("<card-head id={0}>{1}</card-head>\n", 0, "head") +
                string.Format("<text id={0}>{1}</text>\n", 0, "body\ntext"), text);
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
