using Markdown.SubstringHandlers;
using NUnit.Framework;

namespace Markdown.Tests.SubstringHandlers
{
    public class StrongHandler_should
    {
        [TestCase("__text__", ExpectedResult = "<strong>text</strong>", TestName = "Simple text inside double underscore")]
        public string HandleTextInsideDoubleUnderscore(string str)
        {
            var reader = new StringReader(str);
            var strongHanler = new StrongHandler();

            return strongHanler.HandleSubstring(reader);
        }
    }
}
