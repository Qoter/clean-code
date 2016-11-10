using FluentAssertions;
using Markdown.SubstringHandlers;
using NUnit.Framework;

namespace Markdown.Tests.SubstringHandlers
{
    public class CharHandler_should
    {
        [TestCase("123")]
        [TestCase("__")]
        public void CanHandle_Always(string str)
        {
            var reader = new StringReader(str);
            var charHandler = new CharHandler();

            charHandler.CanHandle(reader).Should().BeTrue();
        }

        [TestCase("012")]
        [TestCase("_12")]
        public void ReturnCurrentChar(string str)
        {
            var reader = new StringReader(str);

            var currentChar = reader.CurrentChar.ToString();
            var readedChar = new CharHandler().HandleSubstring(reader);

            readedChar.Should().Be(currentChar);
        }
    }
}
