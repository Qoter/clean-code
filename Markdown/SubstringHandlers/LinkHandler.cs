using System;
using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class LinkHandler : ISubstringHandler
    {
        private readonly MdSettings settings;

        public LinkHandler(MdSettings settings)
        {
            this.settings = settings;
        }

        public string HandleSubstring(StringReader reader)
        {
            if (!CanHandle(reader))
                throw new ArgumentException();

            var linkName = ReadInnerText(reader, '[', ']');
            var linkUrl = ReadInnerText(reader, '(', ')');

            var titleHandler = new FirstWorkHandler(
                new EscapeHandler(),
                new LineBreakHandler(),
                new StrongHandler(settings),
                new EmphasisHandler(settings),
                new CharHandler());
            var processedTitle = titleHandler.HandleUntil(r => r.AtEndOfString, new StringReader(linkName));

            var linkTag = settings.TagProvider.GetTag("a");
            linkTag.AddAttribute("src", PrependBaseUrl(linkUrl));

            return linkTag.Wrap(processedTitle);
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

        public string ReadInnerText(StringReader reader, char leftBrace, char rightBrace)
        {
            var rightBraceIndex = FindRightBrace(reader.String, reader.CurrentIndex, leftBrace, rightBrace);

            reader.Read(1);
            var innerText = reader.Read(rightBraceIndex - reader.CurrentIndex);
            reader.Read(1);

            return innerText;
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
            return settings.BaseUrl == null
                ? relativeUrl
                : new Uri(settings.BaseUrl, relativeUrl).ToString();
        }
    }
}