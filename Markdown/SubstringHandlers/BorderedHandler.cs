using System;
using System.Linq;
using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public abstract class BorderedHandler : ISubstringHandler
    {
        private readonly ISubstringHandler innerTextHandler = new FirstWorkHandler(new EscapeSkipHandler(),
            new LineBreakHandler(), new CharHandler());

        protected abstract string Border { get; }

        public string HandleSubstring(StringReader reader)
        {
            if (!CanHandle(reader))
                throw new InvalidOperationException($"Reader not on border: {Border}");

            var innerText = ReadInnerText(reader);
            return ProcessInnerText(innerText);
        }

        public bool CanHandle(StringReader reader)
            => IsOnOpenedBorder(reader) && ContainsClosedBorderAhead(reader.GetContext(Border).RightReader);

        protected abstract string ProcessInnerText(string innerText);

        private bool ContainsClosedBorderAhead(StringReader reader)
            => reader.FindContextsFor(Border).Any(IsClosedContext);

        private bool IsOnOpenedBorder(StringReader reader)
            => reader.IsLocatedOn(Border) && IsOpenedContext(reader.GetContext(Border));

        private bool IsOnClosedBorder(StringReader reader)
            => reader.IsLocatedOn(Border) && IsClosedContext(reader.GetContext(Border));

        private static bool IsOpenedContext(Context context)
            => !context.InsidePrintable && !context.NextChar.IsWhiteSpace();

        private static bool IsClosedContext(Context context)
            => !context.InsidePrintable && !context.PreviousChar.IsWhiteSpace();

        private void SkipBorder(StringReader reader) => reader.Read(Border.Length);

        private string ReadInnerText(StringReader reader)
        {
            SkipBorder(reader);
            var innerText = innerTextHandler.HandleUntil(IsOnClosedBorder, reader);
            SkipBorder(reader);

            return innerText;
        }
    }
}