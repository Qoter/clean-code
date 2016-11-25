using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Markdown.Infrastructure;
using Markdown.SubstringHandlers;

namespace Markdown
{
    public class MdSettings
    {
        public readonly Uri BaseUrl;
        public static readonly MdSettings Default = new MdSettings(null);

        public MdSettings(Uri baseUrl)
        {
            this.BaseUrl = baseUrl;
        }
    }

    public class Md
    {
        private readonly MdSettings settings;

        public Md(MdSettings settings)
        {
            this.settings = settings;
        }

        public string RenderLineToHtml(string markdownLine)
        {

            var markdownHandler = new FirstWorkHandler(Handlers.Escape, new LinkHandler(settings.BaseUrl), Handlers.Strong, Handlers.Emphasis, Handlers.Char);
            var result = markdownHandler.HandleUntil(r => r.AtEndOfString, new StringReader(markdownLine));
            return Tag.Paragraph.Wrap(result);
        }

        public IEnumerable<string> RenderAllLinesToHtml(IEnumerable<string> markdownLines)
        {
            return markdownLines.Select(RenderLineToHtml);
        }
    }
}