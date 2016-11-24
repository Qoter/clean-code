using System;
using System.Linq;
using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class FirstWorkHandler : ISubstringHandler
    {
        private readonly ISubstringHandler[] handlers;

        public FirstWorkHandler(params ISubstringHandler[] handlers)
        {
            this.handlers = handlers;
        }

        public string HandleSubstring(StringReader reader)
        {
            if (!CanHandle(reader))
                throw new InvalidOperationException("No work readers");

            return handlers
                .First(handler => handler.CanHandle(reader))
                .HandleSubstring(reader);
        }

        public bool CanHandle(StringReader reader)
        {
            return handlers.Any(handler => handler.CanHandle(reader));
        }
    }
}