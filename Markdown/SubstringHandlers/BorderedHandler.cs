using System;
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

            var innerText = ReadInnerText(reader);
            return ProcessInnerText(innerText);
        }

        private string ReadInnerText(StringReader reader)
        {
            SkipBorder(reader);
            var innerText = new CharHandler().HandleUntil(IsOnClosedBorder, reader);
            SkipBorder(reader);

            return innerText;
        }

        public virtual bool CanHandle(StringReader reader)
        {
            if (!IsOnOpenedBorder(reader))
                return false;

            var openedContext = reader.GetContext(Border);
            return openedContext.RightReader
                .FindContextsFor(Border)
                .Any(IsClosedContext);
        }

        private bool IsOnOpenedBorder(StringReader reader) => reader.IsLocatedOn(Border) && IsOpenedContext(reader.GetContext(Border));
        private bool IsOnClosedBorder(StringReader reader) => reader.IsLocatedOn(Border) && IsClosedContext(reader.GetContext(Border));

        private static bool IsOpenedContext(Context context) => !context.InsidePrintable && !context.NextChar.IsWhiteSpace();
        private static bool IsClosedContext(Context context) => !context.InsidePrintable && !context.PreviousChar.IsWhiteSpace();

        private void SkipBorder(StringReader reader) => reader.Read(Border.Length);
    }
}