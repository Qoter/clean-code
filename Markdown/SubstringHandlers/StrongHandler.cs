using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class StrongHandler : BorderedHandler
    {
        protected override string Border { get; } = "__";
        private readonly TagProvider tagProvider;
        private FirstWorkHandler innerTextHandler;

        public StrongHandler(TagProvider tagProvider)
        {
            this.tagProvider = tagProvider;
            innerTextHandler = new FirstWorkHandler(new EscapeHandler(), new EmphasisHandler(tagProvider), new CharHandler());
        }

        protected override string ProcessInnerText(string innerText)
        {
            var processedInner = innerTextHandler.HandleUntil(r => r.AtEndOfString, new StringReader(innerText));
            return tagProvider.GetTag("strong").Wrap(processedInner);
        }
    }
}