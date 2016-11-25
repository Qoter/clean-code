using System;
using System.Collections.Generic;
using System.Linq;
using Markdown.Infrastructure;
using Markdown.SubstringHandlers;

namespace Markdown
{
    public class Md
    {
        private readonly ISubstringHandler markdownHandler;
        private readonly MdSettings settings;

        public Md(MdSettings settings)
        {
            this.settings = settings;

            markdownHandler = new FirstWorkHandler(
                new EscapeHandler(),
                new LineBreakHandler(),
                new LinkHandler(settings.TagProvider, settings.BaseUrl),
                new StrongHandler(settings.TagProvider),
                new EmphasisHandler(settings.TagProvider),
                new CharHandler());
        }

        public string RenderParagraphToHtml(string paragraph)
        {
            var result = markdownHandler.HandleUntil(r => r.AtEndOfString, new StringReader(paragraph));
            return settings.TagProvider.GetTag("p").Wrap(result);
        }

        public string RenderTextToHtml(string markdownText)
        {
            var renderedParagraphs = SplitIntoParagraphs(markdownText)
                .Where(p => !string.IsNullOrEmpty(p))
                .Select(RenderParagraphToHtml);

            return string.Join("", renderedParagraphs);
        }

        private static IEnumerable<string> SplitIntoParagraphs(string markdownText)
        {
            var lines = markdownText.Split(new[] {"\r\n"}, StringSplitOptions.None);

            var currentParagrapgLines = new List<string>();

            foreach (var line in lines)
            {
                if (line.All(char.IsWhiteSpace))
                {
                    yield return string.Join("\r\n", currentParagrapgLines);
                    currentParagrapgLines.Clear();
                }
                else
                {
                    currentParagrapgLines.Add(line);
                }
            }

            yield return string.Join("\r\n", currentParagrapgLines);
        }
    }
}