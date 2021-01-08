using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Parser.Disassemble;
using Parser.Helpers;
using Parser.HtmlObjects;
using Parser.Tags;

namespace Parser
{
    class MarkdownParser
    {
        public static ServiceProvider serviceProvider { get; set; }

        public MarkdownParser()
        {
            var services = new ServiceCollection();

            // Disassemble
            services.AddTransient<IDisassemble, BodyDisassemble>();

            // Helpers
            services.AddSingleton<IDepth,Depth>();
            services.AddSingleton<LineModifier>();
            services.AddSingleton<Parser.Helpers.Index>();
            services.AddSingleton<Seeker>();

            // HtmlObjects
            services.AddTransient<ListItem>();
            services.AddSingleton<Code>();
            services.AddSingleton<InlineCode>();
            services.AddSingleton<Text>();

            // Tags
            services.AddTransient<ITag, TestTags>();

            serviceProvider = services.BuildServiceProvider();
            
        }

        public string ParsePage(string fileName)
        {
            //var lines = System.IO.File.ReadAllLines(fileName);
            var text = serviceProvider.GetService<IDisassemble>();

            var lines = new string[] { "Header","----",
            "Some text etxt ttt tt", " text ",
            "\tc code", "\t ls -ahl","    sudo apt update", "text" ,"----"};

            lines = new string[] { "head", "----", "body", "\tsub-head" ,"\t----", "\tsub-body", "\tsub-head1" ,"\t----", "\tsub-body1", "\tsub-head2" ,"\t----", "\tsub-body2",
                "head1", "----", "body1", "\tsub-head" ,"\t----", "\tsub-body", "\tsub-head1" ,"\t----", "\tsub-body1", "\tsub-head2" ,"\t----", "\tsub-body2",
                "head2", "----", "body2", "\tsub-head" ,"\t----", "\tsub-body", "\tsub-head1" ,"\t----", "\tsub-body1", "\tsub-head2" ,"\t----", "\tsub-body2"};

            Console.WriteLine(text.Disassemble(lines,0));
            // for()
            //text = new BodyDisassemble(ref tags, ref lines).Disassemble(0);

            return "";
        }

        public string ParseTableOfContents(string tableOfContents)
        {
            // Огдавление

            return "";
        }
    }
}
