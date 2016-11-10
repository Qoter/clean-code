using System;
using FluentAssertions;
using Markdown.SubstringReaders;
using NUnit.Framework;

namespace Markdown.Tests.SubstringReaders
{
    [TestFixture]
    public class EmphasisReader_should
    {
        [TestCase("_text_", ExpectedResult = "<em>text</em>")]
        [TestCase("_a\\_b_", ExpectedResult = "<em>a_b</em>", Description = "Ignore escape symbols")]
        [TestCase("_a_a_", ExpectedResult = "<em>a_a</em>", Description = "Ignore undercore inside word")]
        [TestCase("_b a_a b_", ExpectedResult = "<em>b a_a b</em>", Description = "Ignore undercore inside word")]
        public string ReadTextInsideUnderscore(string str)
        {
            var tokenizer = new Tokenizer(str);
            var emphasisReader = new EmphasisReader();

            return emphasisReader.ReadSubstring(tokenizer);
        }

        [TestCase("_ hello_")]
        public void CanNotRead_UnderscoreWithWhiteSpace(string str)
        {
            var tokenizer = new Tokenizer(str);
            var emphasisReader = new EmphasisReader();

            emphasisReader.CanReadSubsting(tokenizer).Should().BeFalse();
        }

        [Test]
        public void ThrowsInvalidOperation_IfTryReadFromNotUnderscore()
        {
            var tokenizer = new Tokenizer("abc");
            var emphasisReader = new EmphasisReader();

            Action tryReadFromNotUnderscore = () => emphasisReader.ReadSubstring(tokenizer);

            tryReadFromNotUnderscore.ShouldThrow<InvalidOperationException>();
        }
    }
}
