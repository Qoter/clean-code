using FluentAssertions;
using Markdown.Infrastructure;
using Markdown.SubstringHandlers;
using NUnit.Framework;

namespace Markdown.Tests.SubstringHandlers
{
    public class LineBreakHandler_should
    {
        [Test]
        public void ReplaceDoubleSpaceWithCrlfOnBreakTag()
        {
            var reader = new StringReader("  \r\n");
            var handler = new LineBreakHandler();

            handler.HandleSubstring(reader).Should().Be("<br />");

        }
    }
}
