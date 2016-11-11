using Markdown.Infrastructure;
using Markdown.SubstringHandlers;

namespace Markdown
{
    public class Md
	{
		public string RenderToHtml(string markdown)
		{
		    var markdownHandler = FirstWorkHandler.CreateFrom(Handlers.Escape, Handlers.Strong, Handlers.Emphasis, Handlers.Char);
		    return markdown.HandleWith(markdownHandler);
		}
	}
}