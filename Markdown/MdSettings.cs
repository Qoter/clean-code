using System;
using Markdown.Infrastructure;

namespace Markdown
{
    public class MdSettings
    {
        public readonly Uri BaseUrl;
        public readonly TagProvider TagProvider;

        public static readonly MdSettings Default = new MdSettings();

        public MdSettings(Uri baseUrl=null, string cssClass=null)
        {
            BaseUrl = baseUrl;
            TagProvider = new TagProvider(cssClass);
        }
    }
}