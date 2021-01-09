using Parser.Helpers;
using Parser.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.HtmlObjects
{
    public class InlineCode : IHtmlObject
    {
        private ITag _tag;
        private Seeker _seeker;

        private int index;

        public InlineCode(ITag tag, Seeker seeker)
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

            var result = _seeker.InsertTagsInLine(Data[index], _tag.tags[Tag.InlineCode][0], "````");

            if (result == Data[index])
            {
                return null;
            }

            return result;
        }       
    }
}
