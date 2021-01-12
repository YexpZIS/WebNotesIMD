using Parser.Helpers;
using Parser.HtmlObjects;
using Parser.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Parser.TableOfContents
{
    public class ButtonToPage : IHtmlObject
    {
        private string[] symbols = new string[] { "* [" ,"](",")"};

        private string Name = "";
        private string Path = "";

        public ButtonToPage(ITag tag, Seeker seeker) : base(tag, seeker)
        {

        }

        public override string isHtmlObject(string[] Data, int index, int depth)
        {
            string button = _seeker.RemovePrefix(Data[index], depth);

            if (button.StartsWith(symbols[0])) 
            {
                Parse(button);
                return GetHtml();
            }

            return null;
        }

        private void Parse(string line)
        {
            line = line.Substring(symbols[0].Length);
            int i = line.IndexOf(symbols[1]);

            Name = line.Substring(0,i);
            line = line.Substring(i+symbols[1].Length);

            i = line.IndexOf(symbols[2]);
            Path = line.Substring(0,i);
        }

        private string GetHtml()
        {
            return string.Format(_tags.tags[Tag.Summary][1], Name, Path, "{2}");
        }
    }
}
