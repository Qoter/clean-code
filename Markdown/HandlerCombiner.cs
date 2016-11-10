using System;
using System.Linq;
using System.Text;

namespace Markdown
{
    public class HandlerCombiner
    {
        private readonly ISubstringHandler[] handlers;

        private Func<StringReader, bool> isEndOfSubstring;

        public HandlerCombiner(params ISubstringHandler[] handlers)
        {
            this.handlers = handlers;
        }

        public string HandleSubstring(StringReader reader)
        {
            var substringBuilder = new StringBuilder();
            while (!reader.AtEndOfString && !isEndOfSubstring.Invoke(reader))
            {
                var firstCanHandle = handlers.First(handler => handler.CanHandle(reader));
                substringBuilder.Append(firstCanHandle.HandleSubstring(reader));
            }

            return substringBuilder.ToString();
        }

        public void SetStopRule(Func<StringReader, bool> predicate)
        {
            isEndOfSubstring = predicate;
        }
    }
}