using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Parser.Disassemble;
using Parser.Helpers;
using Parser.HtmlObjects;
using Parser.TableOfContents;
using Parser.Tags;

namespace Parser
{
    public class MarkdownParser
    {
        public static ServiceProvider serviceProvider { get; set; }

        public MarkdownParser()
        {
            var services = new ServiceCollection();

            // Disassemble
            services.AddTransient<BodyDisassemble>(); // Transient
            services.AddTransient<SummaryDisassemble>(); // Transient

            // Helpers
            services.AddSingleton<IDepth,Depth>();
            services.AddSingleton<LineModifier>();
            services.AddSingleton<Parser.Helpers.Index>();
            services.AddSingleton<Seeker>();

            // HtmlObjects
            services.AddSingleton<Head>();
            services.AddTransient<ListItem>(); // Transient
            services.AddSingleton<Code>();
            services.AddSingleton<InlineCode>();
            services.AddSingleton<Text>();

            // TableOfContents
            services.AddSingleton<BookTitle>();
            services.AddSingleton<ButtonToPage>();
            services.AddSingleton<TextPlug>();
            services.AddTransient<FolderItem>(); // Transient

            // Tags
            //services.AddSingleton<ITag,WebNotesTags>();
            services.AddSingleton<ITag,TestTags>();

            serviceProvider = services.BuildServiceProvider();
            
        }

        public string ParsePage(string fileName)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            string html = "Errore";
            try
            {
                var lines = System.IO.File.ReadAllLines(System.IO.Directory.GetCurrentDirectory() + fileName);

                var parser = serviceProvider.GetService<BodyDisassemble>();
                var tags = serviceProvider.GetService<ITag>();
                tags.ResetId();

                html = parser.Disassemble(lines, 0); 
            }
            catch { }

            stopwatch.Stop();
            return html + "<br>Milliseconds: " + stopwatch.ElapsedMilliseconds+
                "<br>TagId: "+ serviceProvider.GetService<ITag>().id;
        }

        public string ParseTableOfContents(string tableOfContents)
        {
            // Оглавление
            var summary = serviceProvider.GetService<SummaryDisassemble>();
            var lines = System.IO.File.ReadAllLines(System.IO.Directory.GetCurrentDirectory() + tableOfContents);
            string text = summary.Disassemble(lines, 0);

            return text;
        }
    }
}
