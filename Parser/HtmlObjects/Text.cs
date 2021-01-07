using Parser.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.HtmlObjects
{
    public class Text : IHtmlObject
    {
        private ITag _tag;
        private int index;

        public Text(ITag tag)
        {
            _tag = tag;
        }

        public int GetIndex()
        {
            return index + 1;
        }

        public string isHtmlObject(string[] Data, int index)
        {
            this.index = index;
            return string.Format(_tag.tags[Tag.Text][0],Data[index]);
        }
    }
}
