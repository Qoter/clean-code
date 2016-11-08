using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class EscapeHandler_should
    {
        [TestCase(@"\1", 0, ExpectedResult = true)]
        [TestCase(@"1\1", 1, ExpectedResult = true)]
        [TestCase(@"\", 0, ExpectedResult = true)]
        [TestCase(@"\\", 0, ExpectedResult = true)]
        [TestCase(@"123", 0, ExpectedResult = false)]
        public bool CanHandleReturnTrue_OnBackslash(string str, int backslashIndex)
        {
            return new EscapeHandler().CanHandle(str, backslashIndex);
        }

        [TestCase(@"\\", 0, ExpectedResult = @"\")]
        [TestCase(@"\_", 0, ExpectedResult = @"_")]
        public string HandleSymbol_AfterBackslash(string str, int startIndex)
        {
            return new EscapeHandler().Handle(str, ref startIndex);
        }

        [TestCase(@"\12", 0, ExpectedResult = 2)]
        [TestCase(@"0\23", 1, ExpectedResult = 3)]
        public int HandleSymbolMoveIndex_AfterBackslas(string str, int startIndex)
        {
            new EscapeHandler().Handle(str, ref startIndex);
            return startIndex;
        }
    }
}
