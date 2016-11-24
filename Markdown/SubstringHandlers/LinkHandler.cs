using System;
using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class LinkHandler : BorderedHandler
    {
        protected override string Border { get; } = "[";
        protected string HandleBeforeClosedBorder(StringReader reader)
        {
            var title = Handlers.TextWithStorngAndEmphasis.HandleUntil(r => r.IsLocatedOn("]"), reader);
            reader.Read(2);
            var link = Handlers.Char.HandleUntil(r => r.IsLocatedOn(")"), reader);

            return $"<a src='{link}'>{title}</a>";
        }

        protected override string ProcessInnerText(string innerText)
        {
            throw new NotImplementedException();
        }


        protected override bool IsOnClosedBorder(StringReader reader)
        {
            return reader.IsLocatedOn(")");
        }

        public override bool CanHandle(StringReader reader)
        {
            return reader.IsLocatedOn(Border);
        }
    }
}
