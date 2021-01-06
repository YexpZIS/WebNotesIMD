using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Parser
{
    class MarkdownParser
    {
        public string ParsePage(string fileName)
        {
            //var lines = System.IO.File.ReadAllLines(fileName);

            var lines = new string[] { "Header","----",
            "Some text etxt ttt tt", " text ",
            "\tc code", "\t ls -ahl","    sudo apt update", "text" };

            // for()
            //text = new BodyDisassemble(ref tags, ref lines).Disassemble(0);

            return "";
        }
    }
}
