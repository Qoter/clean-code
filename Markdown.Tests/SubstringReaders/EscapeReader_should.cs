using Markdown.SubstringReaders;
using NUnit.Framework;

namespace Markdown.Tests.SubstringReaders
{
    [TestFixture]
    public class EscapeReader_should
    {
        [TestCase(@"\\", ExpectedResult = @"\")]
        [TestCase(@"\_", ExpectedResult = @"_")]
        [TestCase(@"\a", ExpectedResult = @"a")]
        public string ReadSymbol_AfterBackslash(string str)
        {
            var tokenizer = new Tokenizer(str);
            var escapeReader = new EscapeReader();

            return escapeReader.ReadSubstring(tokenizer);
        }
    }
}
