using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class StrongHandler : BorderedTagHandler
    {
        protected override string Border { get; } = "__";
        protected override Tag Tag { get; } = Tag.Strong;

        protected override string HandleBeforeClosedBorder(StringReader reader)
        {
            var simpleTextReader = new FirstWorkHandler(new EscapeHandler(), new CharHandler());
            simpleTextReader.SetStopRule(IsOnClosedBorder);
            var innerText = simpleTextReader.HandleSubstring(reader);
            
            var emphasisHandler = new FirstWorkHandler(new EscapeHandler(), new EmphasisHandler(), new CharHandler());
            return emphasisHandler.HandleSubstring(new StringReader(innerText));
        }

    }
}