using System;
using FluentAssertions;
using Markdown.SubstringHandlers;
using NUnit.Framework;

namespace Markdown.Tests.SubstringHandlers
{
    [TestFixture]
    public class EmphasisHandler_should
    {
        [TestCase("_text_", ExpectedResult = "<em>text</em>", TestName = "Word inside underscore")]
        [TestCase("_a\\_b_", ExpectedResult = "<em>a_b</em>", TestName = "Ignore escape symbols")]
        [TestCase("_a_a_", ExpectedResult = "<em>a_a</em>", TestName = "Ignore underscore inside word")]
        [TestCase("_b a_a b_", ExpectedResult = "<em>b a_a b</em>", TestName = "Ignore undercore inside word")]
        [TestCase("_a _bc_", ExpectedResult = "<em>a _bc</em>", TestName = "Ignore underscore after whitespace")]
        [TestCase("_abc", ExpectedResult = "_abc", TestName = "Not closed underscore")]
        public string ReadTextInsideUnderscore(string str)
        {
            var reader = new StringReader(str);
            var emphasisHandler = new EmphasisHandler();

            return emphasisHandler.HandleSubstring(reader);
        }

        [Test]
        public void CanNotHandle_UnderscoreWithWhiteSpace()
        {
            var reader = new StringReader("_ hello_");
            var emphasisHandler = new EmphasisHandler();

            emphasisHandler.CanHandle(reader).Should().BeFalse();
        }

        [Test]
        public void CanNotHandle_UnderscoreInsideWord()
        {
            var reader = new StringReader("hello_world_");
            reader.Read(5);
            var emphasisHandler = new EmphasisHandler();

            emphasisHandler.CanHandle(reader).Should().BeFalse();
        }

        [Test]
        public void ThrowsInvalidOperation_IfTryHandleWithNotUnderscore()
        {
            var reader = new StringReader("abc");
            var emphasisHandler = new EmphasisHandler();

            Action tryReadFromNotUnderscore = () => emphasisHandler.HandleSubstring(reader);

            tryReadFromNotUnderscore.ShouldThrow<InvalidOperationException>();
        }
    }
}
