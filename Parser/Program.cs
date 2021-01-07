using System;

namespace Parser
{
    class Program
    {
        private static MarkdownParser _parser;

        static void Main(string[] args)
        {
            _parser = new MarkdownParser();

            _parser.ParsePage("index.md");

            Console.WriteLine("Hello World!");
        }
    }
}
