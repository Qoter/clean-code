using FluentAssertions;
using Markdown.Infrastructure;
using Markdown.SubstringHandlers;
using NUnit.Framework;

namespace Markdown.Tests.SubstringHandlers
{
    public class EscapeSkipHandler_should
    {
        [TestCase("\\a")]
        [TestCase("\\ab")]
        [TestCase("\\\\b")]
        public void ReturnSymbolWithBackslash(string str)
        {
            var reader = new StringReader(str);
            var handler = new EscapeSkipHandler();

            handler.HandleSubstring(reader).Should().Be(str.Substring(0, 2));
        }
    }
}