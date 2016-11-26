using Markdown.Infrastructure;
using Markdown.SubstringHandlers;
using NUnit.Framework;

namespace Markdown.Tests.SubstringHandlers
{
    public class HeaderHandler_should
    {
        [TestCase("#hello", ExpectedResult = "<h1>hello</h1>", TestName = "Simple header first size")]
        [TestCase("######hello", ExpectedResult = "<h6>hello</h6>", TestName = "Simple header six size")]
        [TestCase("#_hello_", ExpectedResult = "<h1><em>hello</em></h1>", TestName = "Header with emphasis")]
        [TestCase("#__hello__", ExpectedResult = "<h1><strong>hello</strong></h1>", TestName = "Header with strong")]
        [TestCase("#[hello](world)", ExpectedResult = "<h1><a href='world'>hello</a></h1>", TestName = "Header with link")]
        [TestCase("#hello\r\nworld", ExpectedResult = "<h1>hello</h1>", TestName = "Handle only before new line")]
        public string HandleHeader(string str)
        {
            var reader = new StringReader(str);
            var handler = new HeaderHandler(MdSettings.Default);

            return handler.HandleSubstring(reader);
        }
    }
}
