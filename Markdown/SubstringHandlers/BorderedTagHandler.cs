using System;
using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public abstract class BorderedTagHandler : ISubstringHandler
    {
        protected abstract string Border { get; }
        protected abstract Tag Tag { get; }

        private readonly FirstWorkHandler simpleTextHandler;

        private readonly ISubstringHandler innerHandler;

        protected BorderedTagHandler(ISubstringHandler innerHandler) : this()
        {
            this.innerHandler = innerHandler;
        }

        protected BorderedTagHandler()
        {
            simpleTextHandler = new FirstWorkHandler(new EscapeHandler(), new CharHandler());
            simpleTextHandler.SetStopRule(IsOnClosedBorder);
        }

        public string HandleSubstring(StringReader reader)
        {
            if (!CanHandle(reader))
                throw new InvalidOperationException("Can't read emphasis substring");

            SkipBorder(reader);

            var innerText = simpleTextHandler.HandleSubstring(reader);
            innerText = HandlerInnerText(innerText);

            return TrySkipClosedBorder(reader)
                ? Tag.Wrap(innerText)
                : Border + innerText;
        }

        private string HandlerInnerText(string innerText)
        {
            if (innerHandler == null)
                return innerText;

            var handler = new FirstWorkHandler(new EscapeHandler(), innerHandler, new CharHandler());
            return handler.HandleSubstring(new StringReader(innerText));
        }

        public bool CanHandle(StringReader reader)
        {
            if (!reader.IsLocatedOn(Border))
                return false;

            var borderContext = reader.GetContext(Border);
            return !borderContext.InsidePrintable && !borderContext.NextChar.IsWhiteSpace();
        }

        private bool IsOnClosedBorder(StringReader reader)
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
