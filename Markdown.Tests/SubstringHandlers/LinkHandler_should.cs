using Markdown.Infrastructure;
using Markdown.SubstringHandlers;
using NUnit.Framework;

namespace Markdown.Tests.SubstringHandlers
{
    class LinkHandler_should
    {
        [TestCase("[hello](http://hello.com)", ExpectedResult = "<a src='http://hello.com'>hello</a>", TestName = "Simple link")]
        [TestCase("[ _hello_ ](http://hello.com)", ExpectedResult = "<a src='http://hello.com'><em>hello</em></a>", TestName = "Link with emphasis")]
        public string HandleLink(string str)
        {
            var reader = new StringReader(str);
            var linkHandler = new  LinkHandler();

            return linkHandler.HandleSubstring(reader);
        }
    }
}
