using NUnit.Framework;
using Parser.Helpers;

namespace Tests.Parser.Helpers
{
    class Depth_Tests
    {
        private IDepth _depth;

        [SetUp]
        public void Initr()
        {
            _depth = new Depth();
        }

        // Arrange
        [TestCase("", 0)]
        [TestCase("Text", 0)]
        [TestCase(" Text", 0)]
        [TestCase("  Text", 0)]
        [TestCase("   Text", 0)]
        [TestCase("    Text", 1)]
        [TestCase("\tText", 1)]
        [TestCase("  \t  Text", 2)]
        [TestCase("T e x t ", 0)]
        [TestCase("test\t ", 0)]
        [TestCase("\t\t\t\t\tText", 5)]
        public void GetDepth_Test(string text, int depth)
        {
            // Act
            int calculated_depth = _depth.GetDepth(text);
            // Assert
            Assert.AreEqual(depth, calculated_depth);
        }

        // Arrange
        [TestCase("", 0, "")]
        [TestCase(" test", -1, " test")]
        [TestCase(" test", 1, " test")]
        [TestCase("\ttest", 1, "test")]
        [TestCase("\t\ttest", 1, "\ttest")]
        [TestCase("    test", 1, "test")]
        [TestCase("     test", 1, " test")]
        [TestCase("    \t\t test", 3, " test")]
        public void RemoveTabsAndSpacesBeforeText_Test(string line, int depth, string result)
        {
            // Act
            string clear_text = _depth.RemoveTabsAndSpacesBeforeText(line, depth);
            // Assert
            Assert.AreEqual(result, clear_text);
        }
    }
}
