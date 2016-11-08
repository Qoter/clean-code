using System;

namespace Markdown
{
    public class HandlerCombiner : ISubstringHandler
    {
        private readonly ISubstringHandler[] innerHandlers;
        private string startSubstring = "";

        public HandlerCombiner(params ISubstringHandler[] innerHandlers)
        {
            this.innerHandlers = innerHandlers;
        }

        public HandlerCombiner WithStartSubstring(string substring)
        {
            startSubstring = substring;
            return this;
        }

        public string Handle(string str, ref int startIndex)
        {
            throw new NotImplementedException();
        }

        public bool CanHandle(string str, int startIndex)
        {
            return false;
        }
    }
}