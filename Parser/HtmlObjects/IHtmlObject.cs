using Parser.Helpers;
using Parser.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.HtmlObjects
{
    public abstract class IHtmlObject
    {
        protected ITag _tags;
        protected Seeker _seeker;
        protected int index { get; set; }

        public IHtmlObject(ITag tags, Seeker seeker)
        {
            _tags = tags;
            _seeker = seeker;
        }
        public abstract string isHtmlObject(string[] Data, int index, int depth);
        public int GetIndex()
        {
            return index;
        }
    }
}
