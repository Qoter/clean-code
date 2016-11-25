using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class EmphasisHandler : BorderedHandler
    {
        protected override string Border { get; } = "_";
        private readonly TagProvider tagProvider;

        public EmphasisHandler(TagProvider tagProvider)
        {
            this.tagProvider = tagProvider;
        }

        protected override string ProcessInnerText(string innerText)
        {
            var processedInner =  new FirstWorkHandler(new EscapeHandler(), new CharHandler()).HandleUntil(r => r.AtEndOfString, new StringReader(innerText));
            return tagProvider.GetTag("em").Wrap(processedInner);
        }
    }
}