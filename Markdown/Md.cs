using Markdown.Infrastructure;
using Markdown.SubstringHandlers;

namespace Markdown
{
    public class Md
	{
		public string RenderToHtml(string markdown)
		{
            var mdReader = new FirstWorkHandler(new EscapeHandler(), new StrongHandler(), new EmphasisHandler(), new CharHandler());
		    return mdReader.HandleSubstring(new StringReader(markdown));
		}
	}
}