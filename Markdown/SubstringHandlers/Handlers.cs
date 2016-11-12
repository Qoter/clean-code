namespace Markdown.SubstringHandlers
{
    public static class Handlers
    {
        public static readonly FirstWorkHandler TextWithEscaped = new FirstWorkHandler(new EscapeHandler(), new CharHandler());
        public static readonly StrongHandler Strong = new StrongHandler();
        public static readonly EmphasisHandler Emphasis = new EmphasisHandler();
        public static readonly EscapeHandler Escape = new EscapeHandler();
        public static readonly CharHandler Char = new CharHandler();
    }
}