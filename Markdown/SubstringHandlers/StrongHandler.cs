using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class StrongHandler : BorderedHandler
    {
        protected override string Border { get; } = "__";

        protected override string ProcessInnerText(string innerText)
        {
            var processedInner = new FirstWorkHandler(Handlers.Escape, Handlers.Emphasis, Handlers.Char)
                .HandleUntil(r => r.AtEndOfString, new StringReader(innerText));
            return Tag.Strong.Wrap(processedInner);
        }
    }
}