using System;
using System.IO;
using Fclp;

namespace Markdown.Client
{
    class Program
    {
        public class MarkdownArguments
        {
            public string InputFileName { get; set; }
            public string OutputFileName { get; set; }
        }

        static void Main(string[] args)
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
            var mdRawLines = File.ReadLines(arguments.InputFileName);
            var htmlLines = Md.RenderAllLinesToHtml(mdRawLines);
            File.WriteAllLines(arguments.OutputFileName, htmlLines);
        }
    }
}
