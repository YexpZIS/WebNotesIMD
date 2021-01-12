using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Parser.Helpers;

namespace Tests.Parser.Helpers
{
    class Seekers_Test
    {
        private Seeker _seeker;

        [OneTimeSetUp]
        public void Init()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IDepth, Depth>();
            services.AddSingleton<Index>();
            services.AddSingleton<LineModifier>();
            services.AddSingleton<Seeker>();

            var provider = services.BuildServiceProvider();

            _seeker = provider.GetService<Seeker>();
        }

        [Test]
        public void check_FindIndex()
        {
            // Arrnge
            var lines = new string[] { "", "----", "", "", "" };
            // Act
            int index = _seeker.FindIndex(ref lines, 0, 0);
            // Assert
            Assert.AreEqual(1, index);
        }
        public void check_next_FindIndex()
        {
            // Arrnge
            var lines = new string[] { "", "", "", "", "----" };
            // Act
            int index = _seeker.FindIndex(ref lines, 0, 0);
            // Assert
            Assert.AreEqual(4, index);
        }

        // Arrange
        [TestCase(new string[] { "", "" },0,0, 0)]
        [TestCase(new string[] { "\t", "\t", "- ", "" }, 0, 0, 2)]
        [TestCase(new string[] { "- ", "\t", "\t", "\t- ", "- " }, 1, 0, 4)]
        [TestCase(new string[] { "- ", "\t", "\t", "*[]() ", "- " }, 1, 0, 3)]
        public void check_FindIndexOnThisDepth(string[] lines,int index, int depth, int result)
        {
            // Act
            int i = _seeker.FindIndexOnThisDepth(ref lines, index,depth ,"- ");
            // Assert
            Assert.AreEqual(result, i);
        }

        // Arrange
        [TestCase("  Text", 0)]
        [TestCase("    Text", 1)]
        [TestCase("\tText", 1)]
        [TestCase("T e x t ", 0)]
        [TestCase("test\t ", 0)]
        [TestCase("\t\t\t\t\tText", 5)]
        public void check_GetDepth(string text, int depth)
        {
            // Act
            int calculated_depth = _seeker.GetDepth(text);
            // Assert
            Assert.AreEqual(depth, calculated_depth);
        }

        // Arrange
        [TestCase("\t", 1, "")]
        [TestCase("code", 1, "code")]
        [TestCase(" test", -1, " test")]
        [TestCase("\ttest", 1, "test")]
        [TestCase("\t\ttest", 1, "\ttest")]
        [TestCase("     test", 1, " test")]
        [TestCase("    \t\t test", 3, " test")]
        public void check_RemovePrefix(string line, int depth, string result)
        {
            // Act
            string clear_text = _seeker.RemovePrefix(line, depth);
            // Assert
            Assert.AreEqual(result, clear_text);
        }

        // Arrange
        [TestCase("","", "</>{0}<.>")]
        [TestCase("text","text", "</>{0}<.>")]
        [TestCase("</>text<.>","````text````", "</>{0}<.>")]
        [TestCase("</>text<.>","````text", "</>{0}<.>")]
        [TestCase("text</><.>", "text````", "</>{0}<.>")]
        [TestCase("text</>text<.>text", "text````text````text", "</>{0}<.>")]
        [TestCase("text0 </>text<.> text", "text0 __text__ text", "</>{0}<.>", "__")]
        public void check_InsertTagsInLine(string result, string line, string tags, string delimiter = "````")
        {
            // Act
            string html = _seeker.InsertTagsInLine(line, tags, delimiter);
            // Assert
            Assert.AreEqual(result, html);
        }
    }
}
