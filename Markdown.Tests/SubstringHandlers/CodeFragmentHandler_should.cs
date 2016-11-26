using Markdown.Infrastructure;
using Markdown.SubstringHandlers;
using NUnit.Framework;

namespace Markdown.Tests.SubstringHandlers
{
    public class CodeFragmentHandler_should
    {
        [TestCase("    hello", ExpectedResult = "<pre><code>hello</code></pre>", TestName = "One line code fragment")]
        [TestCase("    hello\r\n    world", ExpectedResult = "<pre><code>hello\r\nworld</code></pre>", TestName = "Two line code fragment")]
        [TestCase("    hello\r\nworld", ExpectedResult = "<pre><code>hello\r\n</code></pre>", TestName = "Stop if new line not start on 4 space")]
        public string HandleCodeFragment(string str)
        {
            var reader = new StringReader(str);
            var handler = new CodeFragmentHandler(MdSettings.Default);

            return handler.HandleSubstring(reader);
        }
    }
}
