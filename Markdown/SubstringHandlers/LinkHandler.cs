using System;
using Markdown.Infrastructure;
using StringReader = Markdown.Infrastructure.StringReader;

namespace Markdown.SubstringHandlers
{
    public class LinkHandler : ISubstringHandler
    {
        private readonly TagProvider tagProvider;
        private readonly Uri baseUrl;

        public LinkHandler(TagProvider tagProvider, Uri baseUrl=null)
        {
            this.tagProvider = tagProvider;
            this.baseUrl = baseUrl;
        }

        public string HandleSubstring(StringReader reader)
        {
            if (!CanHandle(reader))
                throw new ArgumentException();

            var linkName = ReadInnerText(reader, '[', ']');
            var linkUrl = ReadInnerText(reader, '(', ')');

            var titleHandler = new FirstWorkHandler(
                new EscapeHandler(), 
                new StrongHandler(tagProvider),
                new EmphasisHandler(tagProvider),
                new CharHandler());
            var processedTitle = titleHandler.HandleUntil(r => r.AtEndOfString, new StringReader(linkName));

            var linkTag = tagProvider.GetTag("a");
            linkTag.AddAttribute("src", PrependBaseUrl(linkUrl));

            return linkTag.Wrap(processedTitle);
        }

        public string ReadInnerText(StringReader reader, char leftBrace, char rightBrace)
        {
            var rightBraceIndex = FindRightBrace(reader.String, reader.CurrentIndex, leftBrace, rightBrace);

            reader.Read(1);
            var innerText = reader.Read(rightBraceIndex - reader.CurrentIndex);
            reader.Read(1);

            return innerText;
        }

        public bool CanHandle(StringReader reader)
        {
            if (!reader.IsLocatedOn("["))
                return false;
            var leftSquareIndex = FindRightBrace(reader.String, reader.CurrentIndex, '[', ']');
            if (leftSquareIndex == -1 || leftSquareIndex == reader.String.Length - 1)
                return false;

            if (reader.String[leftSquareIndex + 1] != '(')
                return false;
            var rightCircleIndex = FindRightBrace(reader.String, leftSquareIndex + 1, '(', ')');

            return rightCircleIndex != -1;
        }

        private static int FindRightBrace(string str, int startIndex, char open, char close)
        {
            var balance = 0;
            for (var i = startIndex; i < str.Length; i++)
            {
                if (str[i] == open)
                    balance++;
                else if (str[i] == close)
                    balance--;

                if (balance == 0)
                    return i;
            }
            return -1;
        }

        private string PrependBaseUrl(string relativeUrl)
        {
            return baseUrl == null 
                ? relativeUrl 
                : new Uri(baseUrl, relativeUrl).ToString();
        }
    }
}
