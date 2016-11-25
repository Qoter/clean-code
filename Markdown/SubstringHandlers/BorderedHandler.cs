using System;
using System.Diagnostics;
using System.Linq;
using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public abstract class BorderedHandler : ISubstringHandler
    {
        protected abstract string Border { get; }
        protected abstract string ProcessInnerText(string innerText);

        public string HandleSubstring(StringReader reader)
        {
            if (!CanHandle(reader))
                throw new InvalidOperationException($"Reader not on border: {Border}");

            SkipBorder(reader);
            var innerText = new CharHandler().HandleUntil(IsOnClosedBorder, reader);
            SkipBorder(reader);

            return ProcessInnerText(innerText);
        }

        public virtual bool CanHandle(StringReader reader)
        {
            if (!reader.IsLocatedOn(Border))
                return false;


            var openContext = reader.GetContext(Border);
            if (openContext.InsidePrintable || openContext.NextChar.IsWhiteSpace())
                return false;

            var closeContexts = openContext.RightReader.FindContextsFor(Border);
            return closeContexts.Any(c => !c.InsidePrintable && !c.PreviousChar.IsWhiteSpace());
        }

        protected virtual bool IsOnClosedBorder(StringReader reader)
        {
            if (!reader.IsLocatedOn(Border))
                return false;

            var borderContext = reader.GetContext(Border);
            return !borderContext.InsidePrintable && !borderContext.PreviousChar.IsWhiteSpace();
        }

        private void SkipBorder(StringReader reader)
        {
            reader.Read(Border.Length);
        }
    }
}