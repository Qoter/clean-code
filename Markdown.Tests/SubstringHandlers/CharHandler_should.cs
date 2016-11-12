using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Markdown.Infrastructure;
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
        [TestCase("_12\\__")]
        public void ReadAllCharByChar(string str)
        {
            var reader = new StringReader(str);
            var charHandler = new CharHandler();
            var symbols = Enumerable.Range(0, str.Length).Select(i => charHandler.HandleSubstring(reader));

            string.Join("", symbols).Should().Be(str);
        }
    }
}
