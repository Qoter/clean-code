using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown
{
    public class ReaderCombiner
    {
        private readonly ISubstringReader[] readers;
        public ReaderCombiner(params ISubstringReader[] readers)
        {
            this.readers = readers;
        }

        public string Read(Tokenizer tokenizer)
        {
            var firstWorkReader = readers.First(reader => reader.CanReadSubsting(tokenizer));
            return firstWorkReader.ReadSubstring(tokenizer);
        }
    }
}
