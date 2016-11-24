using System;
using System.Text;
using Markdown.Infrastructure;


namespace Markdown.SubstringHandlers
{
    public static class HandlersExtensions
    {
        public static string HandleUntil(this ISubstringHandler handler, Func<StringReader, bool> isEndOfSubstring, StringReader reader)
        {
            var substrings = new StringBuilder();

            while (!reader.AtEndOfString && !isEndOfSubstring.Invoke(reader))
            {
                substrings.Append(handler.HandleSubstring(reader));
            }

            return substrings.ToString();
        }
    }
}
