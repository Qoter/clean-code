using System;
using System.Linq;
using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class LinkHandler : ISubstringHandler
    {
        public string HandleSubstring(StringReader reader)
        {
            if (!CanHandle(reader))
                throw new ArgumentException();

            var endTitleIndex = FindClose(reader.String, reader.CurrentIndex, '[', ']');

            reader.Read(1);
            var title = reader.Read(endTitleIndex - reader.CurrentIndex);
            reader.Read(1);

            var endUrlIndex = FindClose(reader.String, reader.CurrentIndex, '(', ')');

            reader.Read(1);
            var url = reader.Read(endUrlIndex - reader.CurrentIndex);
            reader.Read(1);

            var processedTitle = Handlers.TextWithStorngAndEmphasis.HandleUntil(r => r.AtEndOfString, new StringReader(title));

            return $"<a src='{url}'>{processedTitle}</a>";
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
    }
}
