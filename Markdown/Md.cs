using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public string RenderParagraphToHtml(string paragraph)
        {
            var markdownHandler = new FirstWorkHandler(
                new EscapeHandler(), 
                new LinkHandler(settings.TagProvider, settings.BaseUrl),
                new StrongHandler(settings.TagProvider), 
                new EmphasisHandler(settings.TagProvider),
                new CharHandler());

            var result = markdownHandler.HandleUntil(r => r.AtEndOfString, new StringReader(paragraph));
            return settings.TagProvider.GetTag("p").Wrap(result);
        }

        public string RenderTextToHtml(string markdownText)
        {
            var rednerdeParagraphs = SplitIntoParagraphs(markdownText)
                .Where(p => !string.IsNullOrEmpty(p))
                .Select(RenderParagraphToHtml);

            return string.Join("", rednerdeParagraphs);
        }

        private IEnumerable<string> SplitIntoParagraphs(string markdownText)
        {
            var lines = markdownText.Split(new[] {"\r\n"}, StringSplitOptions.None);
            var currentParagrapg = new StringBuilder();


            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i].All(char.IsWhiteSpace))
                {
                    yield return currentParagrapg.ToString();
                    currentParagrapg.Clear();
                }
                else
                {
                    currentParagrapg.Append(lines[i]);
                }
            }

            yield return currentParagrapg.ToString();
        }
    }
}