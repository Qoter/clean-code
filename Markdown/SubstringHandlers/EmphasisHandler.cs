using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class EmphasisHandler : BorderedHandler
    {
        protected override string Border { get; } = "_";

        protected override string ProcessInnerText(string innerText)
        {
            var processedInner =  Handlers.TextWithEscaped.HandleUntil(r => r.AtEndOfString, new StringReader(innerText));
            return Tag.Emphasis.Wrap(processedInner);
        }
    }
}