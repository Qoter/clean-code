using System;
using System.IO;
using Fclp;

namespace Markdown.Client
{
    public class Program
    {
        private const string HtmlTemplate = "<html><head><meta charset='utf-8'></head><body>{0}</body></html>";
        private static void Main(string[] args)
        {
            var argsParser = new FluentCommandLineParser<MarkdownArguments>();

            argsParser.Setup(arg => arg.InputFileName)
                .As('i', "input")
                .Required();

            argsParser.Setup(arg => arg.OutputFileName)
                .As('o', "output")
                .SetDefault("result.html");

            var parseResult = argsParser.Parse(args);

            if (parseResult.HasErrors)
            {
                Console.WriteLine(parseResult.ErrorText);
                return;
            }

            try
            {
                RenderToHtml(argsParser.Object);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void RenderToHtml(MarkdownArguments arguments)
        {
            var mdText = File.ReadAllText(arguments.InputFileName);
            var htmlBodyContent = new Md(MdSettings.Default).RenderTextToHtml(mdText);
            var html = string.Format(HtmlTemplate, htmlBodyContent);
            File.WriteAllText(arguments.OutputFileName, html);
        }

        public class MarkdownArguments
        {
            public string InputFileName { get; set; }
            public string OutputFileName { get; set; }
        }
    }
}