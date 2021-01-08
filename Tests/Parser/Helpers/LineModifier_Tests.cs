

using NUnit.Framework;
using Parser.Helpers;
using Parser.Tags;

namespace Tests.Parser.Helpers
{
    class LineModifier_Tests
    {
        private LineModifier _modifier;
        private ITag _tag;

        private string inlineCodeTags;

        private string text;

        [OneTimeSetUp]
        public void Init()
        {
            _modifier = new LineModifier();
            _tag = new TestTags();

            inlineCodeTags = _tag.tags[Tag.InlineCode][0];
        }

        [Test]
        public void SimpleTest()
        {
            // Arrange
            string line = "````text ````";
            // Act
            text = _modifier.InsertTagsInLine(line, inlineCodeTags);
            // Assert
            Assert.AreEqual("<inline-code>text </inline-code>", text);
        }


        // TagAreNotClosed

        [Test]
        public void TagAreNotClosed_AtTheBeginning()
        {
            // Arrange
            string line = "````text df";
            // Act
            text = _modifier.InsertTagsInLine(line, inlineCodeTags);
            // Assert
            Assert.AreEqual("<inline-code>text df</inline-code>", text);
        }

        [Test]
        public void TagAreNotClosed_InTheMiddle()
        {
            // Arrange
            string line = "66022-2-2yyyy ````text todm, as,fmgnafd,gmn";
            // Act
            text = _modifier.InsertTagsInLine(line, inlineCodeTags);
            // Assert
            Assert.AreEqual("66022-2-2yyyy <inline-code>text todm, as,fmgnafd,gmn</inline-code>", text);
        }

        [Test]
        public void TagAreNotClosed_InTheEnd()
        {
            // Arrange
            string line = "some this text ````";
            // Act
            text = _modifier.InsertTagsInLine(line, inlineCodeTags);
            // Assert
            Assert.AreEqual("some this text <inline-code></inline-code>", text);
        }

        
        // Quantity

        [Test]
        public void One_TagInText()
        {
            // Arrange
            string line = "asdfasdf````text ````asdfasdf";
            // Act
            text = _modifier.InsertTagsInLine(line, inlineCodeTags);
            // Assert
            Assert.AreEqual("asdfasdf<inline-code>text </inline-code>asdfasdf", text);
        }

        [Test]
        public void Two_TagsInText()
        {
            // Arrange
            string line = "asdfasdf````text````asdfasdf````tag````";
            // Act
            text = _modifier.InsertTagsInLine(line, inlineCodeTags);
            // Assert
            Assert.AreEqual("asdfasdf<inline-code>text</inline-code>asdfasdf<inline-code>tag</inline-code>", text);
        }

        [Test]
        public void Many_TagsInText()
        {
            // Arrange
            string line = "dsdf````text ````asd````f````````a````sd````f`````";
            // Act
            text = _modifier.InsertTagsInLine(line, inlineCodeTags);
            // Assert
            Assert.AreEqual("dsdf<inline-code>text </inline-code>asd<inline-code>f</inline-code><inline-code>a</inline-code>sd<inline-code>f</inline-code>`", text);
        }

        [Test]
        public void No_TagsInText()
        {
            // Arrange
            string line = "asdfasdf`text asdfasdf";
            // Act
            text = _modifier.InsertTagsInLine(line, inlineCodeTags);
            // Assert
            Assert.AreEqual("asdfasdf`text asdfasdf", text);
        }

        // Different delimitors

        [Test]
        public void Delimitor_OneApostrophe()
        {
            // Arrange
            string line = "asdfasdf`text`asdfasdf";
            // Act
            text = _modifier.InsertTagsInLine(line, inlineCodeTags, "`");
            // Assert
            Assert.AreEqual("asdfasdf<inline-code>text</inline-code>asdfasdf", text);
        }

        [Test]
        public void Delimitor_TwoStars()
        {
            // Arrange
            string line = "asd**fasdf`text`asdfa**sdf";
            // Act
            text = _modifier.InsertTagsInLine(line, inlineCodeTags, "**");
            // Assert
            Assert.AreEqual("asd<inline-code>fasdf`text`asdfa</inline-code>sdf", text);
        }

        [Test]
        public void EmptyLine()
        {
            // Arrange
            string line = "";
            // Act
            text = _modifier.InsertTagsInLine(line, inlineCodeTags);
            // Assert
            Assert.AreEqual("", text);
        }
    }
}
