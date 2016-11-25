namespace Markdown.Infrastructure
{
    public class TagProvider
    {
        private readonly string cssClass;

        public TagProvider(string cssClass = null)
        {
            this.cssClass = cssClass;
        }

        public Tag GetTag(string name)
        {
            var resultTag = new Tag(name);
            if (cssClass != null)
                resultTag.AddAttribute("class", cssClass);

            return resultTag;
        }
    }
}