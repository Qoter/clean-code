using Markdown.Infrastructure;
using Markdown.SubstringHandlers;
using NUnit.Framework;

namespace Markdown.Tests.SubstringHandlers
{
    [TestFixture]
    public class EscapeHandler_should
    {
        [TestCase(@"\\", ExpectedResult = @"\")]
        [TestCase(@"\_", ExpectedResult = @"_")]
        [TestCase(@"\a", ExpectedResult = @"a")]
        public string ReturnSymbol_AfterBackslash(string str)
        {
            var reader = new StringReader(str);
            var escapeHandler = new EscapeHandler();

            return escapeHandler.HandleSubstring(reader);
        }
    }
}
