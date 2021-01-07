using Moq;
using NUnit.Framework;
using Parser.Helpers;

namespace Tests.Parser.Helpers
{
    class Index_Tests
    {
        public class Integration_Tests
        {
            private Index _index;

            private string[] lines;
            private int index;

            [OneTimeSetUp]
            public void Init()
            {
                IDepth depth = new Depth();
                _index = new Index(depth);
            }

            [SetUp]
            public void ClearLocalVariables()
            {
                lines = null;
                index = 0;
            }

            // When in array has not delimiter

            [Test]
            public void Index_InEmptyArray()
            {
                // Arrnge
                lines = new string[] { };
                // Act
                index = _index.FindIndex(ref lines, 0, 0);
                // Assert
                Assert.AreEqual(1, index);
            }

            [Test]
            public void IndexWhenArray_HasNotContainsDelimiter()
            {
                // Arrange
                lines = new string[] { "", "", "", "", "" };
                // Act
                index = _index.FindIndex(ref lines, 0, 0);
                // Assert
                Assert.AreEqual(6, index);
            }

            [Test]
            public void IndexWhenArray_HasNotContainsDelimiter_OnActualDepth()
            {
                // Arrange
                lines = new string[] { "", "", "\t----", "", "" };
                // Act
                index = _index.FindIndex(ref lines, 0, 0);
                // Assert
                Assert.AreEqual(6, index);
            }

            [Test]
            public void IndexWhen_StartIndexAterDelimiter()
            {
                // Arrange
                lines = new string[] { "", "", "\t----", "", "" };
                // Act
                index = _index.FindIndex(ref lines, 3, 0);
                // Assert
                Assert.AreEqual(6, index);
            }

            // Between start and end position

            [Test]
            public void Index_AtFirstPosition()
            {
                // Arrnge
                lines = new string[] { "----", "", "", "", "" };
                // Act
                index = _index.FindIndex(ref lines, 0, 0);
                // Assert
                Assert.AreEqual(0, index);
            }

            [Test]
            public void Index_AtLastPosition()
            {
                // Arrnge
                lines = new string[] { "", "", "", "", "----" };
                // Act
                index = _index.FindIndex(ref lines, 0, 0);
                // Assert
                Assert.AreEqual(4, index);
            }

            [Test]
            public void IndexIn_At7Position()
            {
                // Arrnge
                lines = new string[] { "", "", "", "", "?", "", "", "----", "", "", "", "" };
                // Act
                index = _index.FindIndex(ref lines, 0, 0);
                // Assert
                Assert.AreEqual(7, index);
            }

            [Test]
            public void SkipSomeElement_IndexIn_Between_StartAndEnd()
            {
                // Arrnge
                lines = new string[] { "", "----", "", "", "----", "" };
                // Act
                index = _index.FindIndex(ref lines, 2, 0);
                // Assert
                Assert.AreEqual(4, index);
            }

            // Check if work correct with depth

            [Test]
            public void WhenDepthEquals_Minus_One()
            {
                // If do not find delimiter on same depth returns array length

                // Arrage
                lines = new string[] { "", "----", "text", "\t----" };
                // Act
                index = _index.FindIndex(ref lines, 0, -1);
                // Assert
                Assert.AreEqual(5, index);
            }

            [Test]
            public void WhenDepthEquals_One()
            {
                // Arrage
                lines = new string[] { "", "----", "text", "\t\t----", "\t\t\t----", "", "\t----", "some text" };
                // Act
                index = _index.FindIndex(ref lines, 0, 1);
                // Assert
                Assert.AreEqual(6, index);
            }

            [Test]
            public void WhenDepthEquals_Three()
            {
                // Arrage
                lines = new string[] { "", "----", "text", "\t\t----", "\t\t\t----", "", "\t----", "some text" };
                // Act
                index = _index.FindIndex(ref lines, 0, 3);
                // Assert
                Assert.AreEqual(4, index);
            }

            [Test]
            public void WhenDepthEquals_Ten()
            {
                // Arrage
                lines = new string[] { "", "----", "text", "\t\t----", "\t\t\t----", "\t\t\t\t\t\t\t\t\t\t", "\t----", "\t\t\t\t\t\t\t\t\t\t----", "some text" };
                // Act
                index = _index.FindIndex(ref lines, 0, 10);
                // Assert
                Assert.AreEqual(7, index);
            }

            // Different delimiters

            [Test]
            public void ChangeDelimiter()
            {
                // Arrange
                lines = new string[] { "", "----", "====", "", "==WD_Blue==", "", "" };
                // Act
                index = _index.FindIndex(ref lines, 0, 0, "==WD_Blue==");
                // Assert
                Assert.AreEqual(4, index);
            }

            [Test]
            public void ChangeDelimiterOnSpace()
            {
                // Arrange
                lines = new string[] { "", "----", "====", "  ", "==WD_Blue==", "", "" };
                // Act
                index = _index.FindIndex(ref lines, 0, 0, " ");
                // Assert
                Assert.AreEqual(3, index);
            }

        }





        public class Unit_Tests
        {
            private Index _index;

            private string[] lines;
            private int index;

            [SetUp]
            public void ClearLocalVariables()
            {
                lines = null;
                index = 0;
            }

            [Test]
            public void SimpleTest()
            {
                // Arrange
                Mock<IDepth> depth = new Mock<IDepth>();

                depth.Setup(x => x.GetDepth("")).Returns(0);
                depth.Setup(x => x.GetDepth("----")).Returns(0);

                lines = new string[] { "----", "", "", "", "" };
                var obj = depth.Object;
                _index = new Index(obj);
                // Act
                index = _index.FindIndex(ref lines, 0, 0);
                // Assert
                Assert.AreEqual(0, index);
            }

            [Test]
            public void Index_At2Position()
            {
                // Arrange
                Mock<IDepth> depth = new Mock<IDepth>();

                depth.Setup(x => x.GetDepth("")).Returns(0);
                depth.Setup(x => x.GetDepth("----")).Returns(0);
                depth.Setup(x => x.GetDepth("\t----")).Returns(1);

                lines = new string[] { "\t----", "", "----", "", "" };
                var obj = depth.Object;
                _index = new Index(obj);
                // Act
                index = _index.FindIndex(ref lines, 0, 0);
                // Assert
                Assert.AreEqual(2, index);
            }

            [Test]
            public void ChangeDelimiter()
            {
                // Arrange
                Mock<IDepth> depth = new Mock<IDepth>();

                depth.Setup(x => x.GetDepth("")).Returns(0);
                depth.Setup(x => x.GetDepth("==")).Returns(0);

                lines = new string[] { "==", "", "", "==", "" };
                var obj = depth.Object;
                _index = new Index(obj);
                // Act
                index = _index.FindIndex(ref lines, 0, 0, "==");
                // Assert
                Assert.AreEqual(0, index);
            }
        }
    }
}
