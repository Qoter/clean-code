using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class EmphasisHandler : BorderedHandler
    {
        private readonly MdSettings settings;
        private readonly ISubstringHandler innerTextHandler;

        public EmphasisHandler(MdSettings settings)
        {
            this.settings = settings;
            innerTextHandler = new FirstWorkHandler(
                new EscapeHandler(),
                new LineBreakHandler(),
                new LinkHandler(settings),
                new CharHandler());
        }

        protected override string Border { get; } = "_";

        protected override string ProcessInnerText(string innerText)
        {
            var processedInner = innerTextHandler.HandleUntil(r => r.AtEndOfString, new StringReader(innerText));
            return settings.TagProvider.GetTag("em").Wrap(processedInner);
        }
    }
}