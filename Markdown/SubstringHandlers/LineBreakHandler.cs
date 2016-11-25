using System;
using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class LineBreakHandler : ISubstringHandler
    {
        private const string LineBreakSequence = "  \r\n";

        public string HandleSubstring(StringReader reader)
        {
            if (!CanHandle(reader))
                throw new ArgumentException();

            reader.Read(LineBreakSequence.Length);

            return "<br />";
        }

        public bool CanHandle(StringReader reader)
        {
            return reader.IsLocatedOn(LineBreakSequence);
        }
    }
}