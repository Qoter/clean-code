using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Markdown.Infrastructure;
using Markdown.SubstringHandlers;

namespace Markdown
{
    public class Md
    {
        public static string RenderLineToHtml(string markdownLine)
        {
            var markdownHandler = new FirstWorkHandler(Handlers.Escape, Handlers.Strong, Handlers.Emphasis, Handlers.Char);
            var result = markdownHandler.HandleUntil(r => r.AtEndOfString, new StringReader(markdownLine));
            return Tag.Paragraph.Wrap(result);
        }

        public static IEnumerable<string> RenderAllLinesToHtml(IEnumerable<string> markdownLines)
        {
            return markdownLines.Select(RenderLineToHtml);
        }
    }
}