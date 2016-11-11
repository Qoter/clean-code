using System;
using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public abstract class BorderedTagHandler : ISubstringHandler
    {
        protected abstract string Border { get; }
        protected abstract Tag Tag { get; }

        private readonly FirsWorkHandler innerTextHandler;

        protected BorderedTagHandler(ISubstringHandler[] innerHandlers)
        {
            innerTextHandler = new FirsWorkHandler(innerHandlers);
            innerTextHandler.SetStopRule(IsOnClosedBorder);
        }

        public string HandleSubstring(StringReader reader)
        {
            if (!CanHandle(reader))
                throw new InvalidOperationException("Can't read emphasis substring");

            SkipBorder(reader);
            var innerText = innerTextHandler.HandleSubstring(reader);

            return TrySkipClosedBorder(reader)
                ? Tag.Wrap(innerText)
                : Border + innerText;
        }

        public bool CanHandle(StringReader reader)
        {
            if (!reader.IsLocatedOn(Border))
                return false;

            var borderContext = reader.GetContext(Border);
            return !borderContext.InsideWordOrDigit && !borderContext.NextChar.IsWhiteSpace();
        }

        private bool IsOnClosedBorder(StringReader reader)
        {
            if (!reader.IsLocatedOn(Border))
                return false;

            var borderContext = reader.GetContext(Border);
            return !borderContext.InsideWordOrDigit && !borderContext.PreviousChar.IsWhiteSpace();
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
