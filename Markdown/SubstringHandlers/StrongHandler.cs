using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class StrongHandler : BorderedTagHandler
    {
        protected override string Border { get; } = "__";
        protected override Tag Tag { get; } = Tag.Strong;

        protected override string HandleBeforeClosedBorder(StringReader reader)
        {
            return Handlers.TextWithEscaped
                .WithStopRule(IsOnClosedBorder)
                .HandleSubstring(reader)
                .HandleWith(Handlers.Escape, Handlers.Emphasis, Handlers.Char);
        }

    }
}