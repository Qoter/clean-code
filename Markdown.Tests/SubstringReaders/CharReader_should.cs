using FluentAssertions;
using Markdown.SubstringReaders;
using NUnit.Framework;

namespace Markdown.Tests.SubstringReaders
{
    public class CharReader_should
    {
        [TestCase("123")]
        [TestCase("__")]
        public void CanRead_Always(string str)
        {
            var tokenizer = new Tokenizer(str);
            var charReader = new CharReader();

            charReader.CanReadSubsting(tokenizer).Should().BeTrue();
        }

        [TestCase("012")]
        [TestCase("_12")]
        public void ReturnCurrentChar(string str)
        {
            var tokenizer = new Tokenizer(str);

            var currentChar = tokenizer.CurrentChar.ToString();
            var readedChar = new CharReader().ReadSubstring(tokenizer);

            readedChar.Should().Be(currentChar);
        }
    }
}
