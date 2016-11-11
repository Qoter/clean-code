using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class EmphasisHandler : BorderedTagHandler
    {
        protected override string Border { get; } = "_";
        protected override Tag Tag { get; } = Tag.Emphasis;

        protected override string HandleBeforeClosedBorder(StringReader reader)
        {
            return Handlers.TextWithEscaped
                .WithStopRule(IsOnClosedBorder)
                .HandleSubstring(reader);
        }
    }
}