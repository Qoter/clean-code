namespace Markdown.SubstringHandlers
{
    public static class StringExtensionForHandlers
    {
        public static string HandleWith(this string str, params ISubstringHandler[] handlers)
        {
            var firstWorkHandler = new FirstWorkHandler(handlers);
            return firstWorkHandler.HandleString(str);
        }
    }
}