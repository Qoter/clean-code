using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class StrongHandler : BorderedTagHandler
    {
        protected override string Border { get; } = "__";

        protected override string HandleBeforeClosedBorder(StringReader reader)
        {
            return Handlers.TextWithEscaped
                .WithStopRule(IsOnClosedBorder)
                .HandleSubstring(reader)
                .HandleWith(Handlers.Escape, Handlers.Emphasis, Handlers.Char);
        }

        protected override string WrapIntoTag(string str)
        {
            return Tag.Strong.Wrap(str);
        }
    }
}