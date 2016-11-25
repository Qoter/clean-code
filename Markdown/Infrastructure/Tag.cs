using System;
using System.Collections.Generic;
using System.Linq;

namespace Markdown.Infrastructure
{
    public class Tag
    {
        private readonly string name;

        private readonly List<KeyValuePair<string, string>> attributes = new List<KeyValuePair<string, string>>();

        public Tag(string name)
        {
            this.name = name;
        }

        public void AddAttribute(string key, string value)
        {
            attributes.Add(new KeyValuePair<string, string>(key, value));
        }

        public string Wrap(string content)
        {
            var attributesString = string.Join("", attributes.Select(attr => $" {attr.Key}='{attr.Value}'"));
            return $"<{name}{attributesString}>{content}</{name}>";
        }
    }
}