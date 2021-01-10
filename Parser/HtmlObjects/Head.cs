using Parser.Helpers;
using Parser.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser.HtmlObjects
{
    public class Head : IHtmlObject
    {
        private string[] Data;

        public Head(ITag tag, Seeker seeker) : base(tag, seeker)
        {

        }

        public override string isHtmlObject(string[] Data, int index, int depth)
        {
            this.index = index;
            this.Data = Data;

            if (index == 0)
            {
                string title = GetHead("====");

                if (title != null) 
                {
                    string tags = GetText("----");

                    return GetHtml(title, tags);
                }
                else
                {
                    return null; 
                }
            }

            return null;
        }

        private string GetHead(string delimiter)
        {
            int end = _seeker.FindIndex(ref Data, index, 0, delimiter);

            if (end - 1 != Data.Length) 
            {
                string[] lines = Data.Skip(index).Take(end).ToArray();

                index = end + 1;

                return string.Join("<br>\n", lines);
            }

            return null;
        }
        private string GetText(string delimiter)
        {
            int end = _seeker.FindIndex(ref Data, index, 0, delimiter);
            string[] lines = Data.Skip(index).Take(end-index-1).ToArray();

            index = end - 2;

            return string.Join("<br>\n", lines);
        }

        private string GetHtml(string title, string tags)
        {
            return string.Format(_tags.tags[Tag.Header][0], title) +
                string.Format(_tags.tags[Tag.Header][1], tags);
        }
    }
}
