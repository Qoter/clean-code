using FluentAssertions;
using NUnit.Framework;

namespace Markdown.Tests
{
    public class CharHandler_should
    {
        [TestCase("123", 0)]
        [TestCase("__", 1)]
        public void CanHandle_Always(string str, int index)
        {
            new CharHandler().CanHandle(str, index).Should().BeTrue();
        }

        [TestCase("012", 1, ExpectedResult = "1")]
        public string ReturnCurrentChar(string str, int index)
        {
            return new CharHandler().Handle(str, ref index);
        }

        [TestCase("012", 1, ExpectedResult = 2)]
        public int IncreaseIndex(string str, int index)
        {
            new CharHandler().Handle(str, ref index);
            return index;
        }
    }
}
