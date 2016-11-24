using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class LinkHandler : BorderedTagHandler
    {
        protected override string Border { get; } = "[";
        protected override string HandleBeforeClosedBorder(StringReader reader)
        {
            var title = Handlers.TextWithStorngAndEmphasis.WithStopRule(r => r.IsLocatedOn("]")).HandleSubstring(reader);
            reader.Read(2);
            var link = Handlers.TextHandler.WithStopRule(r => r.IsLocatedOn(")")).HandleSubstring(reader);

            return $"<a src='{link}'>{title}</a>";
        }

        protected override string WrapIntoTag(string str)
        {
            return str;
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
