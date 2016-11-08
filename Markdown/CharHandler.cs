using System;

namespace Markdown
{
    public class CharHandler : ISubstringHandler
    {
        public string Handle(string str, ref int startIndex)
        {
            throw new NotImplementedException();
        }

        public bool CanHandle(string str, int startIndex)
        {
            return true;
        }
    }
}