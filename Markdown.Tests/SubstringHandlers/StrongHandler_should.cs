using FluentAssertions;
using Markdown.Infrastructure;
using Markdown.SubstringHandlers;
using NUnit.Framework;
using NUnit.Framework.Internal.Filters;

namespace Markdown.Tests.SubstringHandlers
{
    public class StrongHandler_should
    {
        [TestCase("__text__", ExpectedResult = "<strong>text</strong>", TestName = "Simple text inside double underscore")]
        [TestCase("__a _b__", ExpectedResult = "<strong>a _b</strong>", TestName = "Ignore one uderscore without close")]
        [TestCase("__ab__cd__", ExpectedResult = "<strong>ab__cd</strong>", TestName = "Igonre double uderscore inside word")]
        [TestCase("__a\\__b__", ExpectedResult = "<strong>a__b</strong>", TestName = "Ignore escape symbols")]
        [TestCase("__b a__a b__", ExpectedResult = "<strong>b a__a b</strong>", TestName = "Ignore double undercore inside word")]
        [TestCase("__a __bc__", ExpectedResult = "<strong>a __bc</strong>", TestName = "Ignore double underscore after whitespace")]
        public string HandleTextInsideDoubleUnderscore(string str)
        {
            var reader = new StringReader(str);
            var strongHanler = new StrongHandler();

            return strongHanler.HandleSubstring(reader);
        }

        [TestCase("abc", TestName = "No underscore")]
        [TestCase("__abc", TestName = "Double underscore without pair")]
        [TestCase("__12__3", TestName = "Double underscore with pair in text")]
        public void CanNotHandle(string str)
        {
            var reader = new StringReader(str);
            var strongHandler = new StrongHandler();

            strongHandler.CanHandle(reader).Should().BeFalse();
        }

        [TestCase("__a _bc_ d__", ExpectedResult = "<strong>a <em>bc</em> d</strong>")]
        public string HandlerTextInsideDoubleUderscore_WithUnderscoreInside(string str)
        {
            var reader = new StringReader(str);
            var strongHandler = new StrongHandler();

            return strongHandler.HandleSubstring(reader);
        }
    }
}
