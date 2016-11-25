using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    class EscapeSkipHandler : ISubstringHandler
    {
        public string HandleSubstring(StringReader reader)
        {
            return reader.Read(2);
        }

        public bool CanHandle(StringReader reader)
        {
            return reader.IsLocatedOn("\\");
        }
    }
}
