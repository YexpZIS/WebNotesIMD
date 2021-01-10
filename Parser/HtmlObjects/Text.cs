using Parser.Helpers;
using Parser.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser.HtmlObjects
{
    public class Text : IHtmlObject
    {
        private string separator = "<br>\n";

        public Text(ITag tags, Seeker seeker) : base(tags, seeker)
        {

        }

        public override string isHtmlObject(string[] Data, int index, int depth)
        {
            this.index = index;

            string clearText = _seeker.RemovePrefix(Data[index], depth);

            var b = Data.Take(index).Count(x => _seeker.RemovePrefix(x, depth) != "");
            var a = Data.Skip(index).Count(x => _seeker.RemovePrefix(x, depth) != "");

            if (clearText == "" && a != 0 && b != 0)
            {
                return separator;
            }
            else
            {
                return string.Format(_tags.tags[Tag.Text][0], clearText);
            }
        }

    }
}
