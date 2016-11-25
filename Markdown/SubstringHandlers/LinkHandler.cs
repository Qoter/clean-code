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

            var endTitleIndex = FindClose(reader.String, reader.CurrentIndex, '[', ']');

            reader.Read(1);
            var linkTitle = reader.Read(endTitleIndex - reader.CurrentIndex);
            reader.Read(1);

            var endUrlIndex = FindClose(reader.String, reader.CurrentIndex, '(', ')');

            reader.Read(1);
            var linkUrl = reader.Read(endUrlIndex - reader.CurrentIndex);
            reader.Read(1);

            var handlerForTitle = new FirstWorkHandler(new EscapeHandler(), new StrongHandler(tagProvider), new EmphasisHandler(tagProvider), new CharHandler());
            var processedTitle = handlerForTitle.HandleUntil(r => r.AtEndOfString, new StringReader(linkTitle));

            var linkTag = tagProvider.GetTag("a");
            linkTag.AddAttribute("src", AddBaseUrl(linkUrl));

            return linkTag.Wrap(processedTitle);
        }

        public bool CanHandle(StringReader reader)
        {
            if (!reader.IsLocatedOn("["))
                return false;


            var closeSquare = FindClose(reader.String, reader.CurrentIndex, '[', ']');
            if (closeSquare == -1 || closeSquare == reader.String.Length - 1)
                return false;

            if (reader.String[closeSquare + 1] != '(')
                return false;

            var closeCircle = FindClose(reader.String, closeSquare + 1, '(', ')');

            return closeCircle != -1;
        }

        private static int FindClose(string str, int startIndex, char open, char close)
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

        private string AddBaseUrl(string relativeUrl)
        {
            return baseUrl == null 
                ? relativeUrl 
                : new Uri(baseUrl, relativeUrl).ToString();
        }
    }
}
