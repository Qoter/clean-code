using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Markdown
{
    public class Tag
    {
        public static readonly Tag Emphasis = new Tag("em");
        public static readonly Tag Strong = new Tag("strong");

        private readonly string tag;
        private Tag(string tag)
        {
            this.tag = tag;
        }

        public string Wrap(string str)
        {
            return $"<{tag}>{str}</{tag}>";
        }
    }
}
