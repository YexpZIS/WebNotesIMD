using Parser.Helpers;
using Parser.HtmlObjects;
using Parser.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.TableOfContents
{
    public class BookTitle : IHtmlObject
    {
        private string symbol = "# ";

        public BookTitle(ITag tag, Seeker seeker) : base(tag, seeker)
        {

        }
        public override string isHtmlObject(string[] Data, int index, int depth)
        {
            if (index == 0)
            {
                int position = _seeker.FindIndex(ref Data, index, 0, symbol);

                return GetTitle(ref Data, position);
            }

            return null;
        }

        private string GetTitle(ref string[] Data, int index)
        {
            if (Data.Length != index - 1)
            {
                string line = Data[index];
                string title = line.Substring(symbol.Length, line.Length - symbol.Length);

                return title;
            }

            return null;
        }
    }
}
