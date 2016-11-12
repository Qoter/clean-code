using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Markdown.SubstringHandlers;

namespace Markdown
{
    public class Md
    {
        public static string RenderLineToHtml(string markdownLine)
        {
            var markdownHandler = FirstWorkHandler.CreateFrom(Handlers.Escape, Handlers.Strong, Handlers.Emphasis, Handlers.Char);
            return markdownLine.HandleWith(markdownHandler);
        }

        public static IEnumerable<string> RenderAllLinesToHtml(IEnumerable<string> markdownLines)
        {
            return markdownLines.Select(RenderLineToHtml);
        }
    }
}