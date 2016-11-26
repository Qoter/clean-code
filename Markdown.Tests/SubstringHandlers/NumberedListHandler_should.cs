using Markdown.Infrastructure;
using Markdown.SubstringHandlers;
using NUnit.Framework;

namespace Markdown.Tests.SubstringHandlers
{
    public class NumberedListHandler_should
    {
        [TestCase("1. first", ExpectedResult = "<ol><li>first</li></ol>", TestName = "List with one item")]
        [TestCase("1. first\r\n1. second", ExpectedResult = "<ol><li>first</li><li>second</li></ol>", TestName = "List with two items")]
        [TestCase("1. first\r\nother", ExpectedResult = "<ol><li>first</li></ol>", TestName = "End on new line without number")]
        public string HandleNumberedList(string str)
        {
            var reader = new StringReader(str);
            var handler = new NumberedListHandler(MdSettings.Default);

            return handler.HandleSubstring(reader);
        }
    }
}
