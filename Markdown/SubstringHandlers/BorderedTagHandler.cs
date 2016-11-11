using System;
using System.Linq;
using System.Text;
using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public abstract class BorderedTagHandler : ISubstringHandler
    {
        protected abstract string Border { get; }
        protected abstract Tag Tag { get; }

        private readonly ISubstringHandler[] innerHandlers;

        protected BorderedTagHandler(params ISubstringHandler[] innerHandlers)
        {
            this.innerHandlers = innerHandlers;
        }

        public string HandleSubstring(StringReader reader)
        {
            if (!CanHandle(reader))
                throw new InvalidOperationException("Can't read emphasis substring");

            SkipBorder(reader);

            var handler2 = new FirstWorkHandler(new EscapeHandler(), new CharHandler());
            handler2.SetStopRule(IsOnClosedBorder);

            var innerText = handler2.HandleSubstring(reader);

            innerText = HandlerInnerText(innerText);

            return TrySkipClosedBorder(reader)
                ? Tag.Wrap(innerText)
                : Border + innerText;
        }

        private string HandlerInnerText(string innerText)
        {
            if (innerHandlers.Length == 0)
                return innerText;

            var handlers = new[] {new EscapeHandler(), innerHandlers.First(), new CharHandler()};
            var reader = new StringReader(innerText);
            var res = new StringBuilder();

            while (!reader.AtEndOfString)
            {
                var h = handlers.First(he => he.CanHandle(reader));
                res.Append(h.HandleSubstring(reader));
            }

            return res.ToString();
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
