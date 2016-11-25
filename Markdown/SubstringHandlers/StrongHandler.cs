using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class StrongHandler : BorderedHandler
    {
        protected override string Border { get; } = "__";
        private readonly TagProvider tagProvider;

        public StrongHandler(TagProvider tagProvider)
        {
            this.tagProvider = tagProvider;
        }

        protected override string ProcessInnerText(string innerText)
        {
            var processedInner = new FirstWorkHandler(new EscapeHandler(), new EmphasisHandler(tagProvider), new CharHandler())
                .HandleUntil(r => r.AtEndOfString, new StringReader(innerText));
            return tagProvider.GetTag("strong").Wrap(processedInner);
        }
    }
}