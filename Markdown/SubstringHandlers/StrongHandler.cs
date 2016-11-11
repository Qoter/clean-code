namespace Markdown.SubstringHandlers
{
    public class StrongHandler : BorderedTagHandler
    {
        protected override string Border { get; } = "__";
        protected override Tag Tag { get; } = Tag.Strong;

        public StrongHandler() : base(new EmphasisHandler())
        {
        }
    }
}