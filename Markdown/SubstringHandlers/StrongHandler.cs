using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class StrongHandler : BorderedHandler
    {
        private readonly MdSettings settings;
        private readonly FirstWorkHandler innerTextHandler;

        public StrongHandler(MdSettings settings)
        {
            this.settings = settings;
            innerTextHandler = new FirstWorkHandler(
                new EscapeHandler(),
                new LinkHandler(settings),
                new EmphasisHandler(settings),
                new CharHandler());
        }

        protected override string Border { get; } = "__";

        protected override string ProcessInnerText(string innerText)
        {
            var processedInner = innerTextHandler.HandleUntil(r => r.AtEndOfString, new StringReader(innerText));
            return settings.TagProvider.GetTag("strong").Wrap(processedInner);
        }
    }
}