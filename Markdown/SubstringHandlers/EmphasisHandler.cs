using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class EmphasisHandler : BorderedTagHandler
    {
        protected override string Border { get; } = "_";

        protected override string HandleBeforeClosedBorder(StringReader reader)
        {
            return Handlers.TextWithEscaped
                .WithStopRule(IsOnClosedBorder)
                .HandleSubstring(reader);
        }

        protected override string WrapIntoTag(string str)
        {
            return Tag.Emphasis.Wrap(str);
        }
    }
}