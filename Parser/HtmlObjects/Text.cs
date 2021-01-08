using Parser.Helpers;
using Parser.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.HtmlObjects
{
    public class Text : IHtmlObject
    {
        private ITag _tag;
        private Seeker _seeker;

        private int index;

        public Text(ITag tag, Seeker seeker)
        {
            _tag = tag;
            _seeker = seeker;
        }

        public int GetIndex()
        {
            return index;
        }

        public string isHtmlObject(string[] Data, int index, int depth)
        {
            this.index = index;
            return string.Format(_tag.tags[Tag.Text][0], _seeker.RemovePrefix(Data[index], depth));
        }
    }
}
