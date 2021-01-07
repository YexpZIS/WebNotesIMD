using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Tags
{
    class WebNotesTags : ITag
    {
        public int id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Dictionary<Tag, string[]> tags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int GetNextId()
        {
            throw new NotImplementedException();
        }
    }
}
