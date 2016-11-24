using System;
using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public abstract class BorderedTagHandler : ISubstringHandler
    {
        protected abstract string Border { get; }

        public string HandleSubstring(StringReader reader)
        {
            if (!CanHandle(reader))
                throw new InvalidOperationException($"Reader not on border: {Border}");

            SkipBorder(reader);
            var innerText = HandleBeforeClosedBorder(reader);

            return TrySkipClosedBorder(reader)
                ? WrapIntoTag(innerText)
                : Border + innerText;
        }

        public virtual bool CanHandle(StringReader reader)
        {
            if (!reader.IsLocatedOn(Border))
                return false;

            var borderContext = reader.GetContext(Border);
            return !borderContext.InsidePrintable && !borderContext.NextChar.IsWhiteSpace();
        }

        protected abstract string HandleBeforeClosedBorder(StringReader reader);

        protected abstract string WrapIntoTag(string str);

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

        private bool TrySkipClosedBorder(StringReader reader)
        {
            if (!IsOnClosedBorder(reader))
                return false;

            SkipBorder(reader);
            return true;
        }
    }
}