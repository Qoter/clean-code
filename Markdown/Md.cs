using System.Collections.Generic;
using System.Linq;
using Markdown.Infrastructure;
using Markdown.SubstringHandlers;

namespace Markdown
{
    public class Md
    {
        private readonly MdSettings settings;

        public Md(MdSettings settings)
        {
            this.settings = settings;
        }

        public string RenderLineToHtml(string markdownLine)
        {
            var markdownHandler = new FirstWorkHandler(
                new EscapeHandler(), 
                new LinkHandler(settings.TagProvider, settings.BaseUrl),
                new StrongHandler(settings.TagProvider), 
                new EmphasisHandler(settings.TagProvider),
                new CharHandler());

            var result = markdownHandler.HandleUntil(r => r.AtEndOfString, new StringReader(markdownLine));
            return settings.TagProvider.GetTag("p").Wrap(result);
        }

        public IEnumerable<string> RenderAllLinesToHtml(IEnumerable<string> markdownLines)
        {
            return markdownLines.Select(RenderLineToHtml);
        }
    }
}