using System;
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
            var innerText = Handlers.Char.HandleUntil(IsOnClosedBorder, reader);
            SkipBorder(reader);

            return ProcessInnerText(innerText);
        }

        public virtual bool CanHandle(StringReader reader)
        {
            // check open border
            if (!reader.IsLocatedOn(Border))
                return false;

            var borderContext = reader.GetContext(Border);
            var canStart = !borderContext.InsidePrintable && !borderContext.NextChar.IsWhiteSpace();
            if (!canStart)
                return false;


            //check close border
            var currentIndex = reader.CurrentIndex;
            currentIndex += Border.Length;

            while (currentIndex < reader.String.Length)
            {
                if (reader.String.StartsWith(Border, currentIndex))
                {
                    var closeBorderContext = reader.GetContextOn(currentIndex, Border);
                    if (!closeBorderContext.InsidePrintable && !closeBorderContext.PreviousChar.IsWhiteSpace())
                    {
                        return true;
                    }
                }
                currentIndex++;
            }
            return false;

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