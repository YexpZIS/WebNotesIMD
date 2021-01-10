using Parser.Helpers;
using Parser.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.HtmlObjects
{
    public class InlineCode : IHtmlObject
    {
        public InlineCode(ITag tags, Seeker seeker) : base(tags, seeker)
        {

        }

        public override string isHtmlObject(string[] Data, int index, int depth)
        {
            this.index = index;

            var result = _seeker.InsertTagsInLine(Data[index], _tags.tags[Tag.InlineCode][0], "`");

            if (result == Data[index])
            {
                return null;
            }

            return result;
        }       
    }
}
