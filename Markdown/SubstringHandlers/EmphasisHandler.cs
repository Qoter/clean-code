using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class EmphasisHandler : BorderedHandler
    {
        protected override string Border { get; } = "_";
        private readonly TagProvider tagProvider;
        private readonly ISubstringHandler innerTextHandler;

        public EmphasisHandler(TagProvider tagProvider)
        {
            this.tagProvider = tagProvider;
            innerTextHandler = new FirstWorkHandler(new EscapeHandler(), new CharHandler());
        }

        protected override string ProcessInnerText(string innerText)
        {
            var processedInner =  innerTextHandler.HandleUntil(r => r.AtEndOfString, new StringReader(innerText));
            return tagProvider.GetTag("em").Wrap(processedInner);
        }
    }
}