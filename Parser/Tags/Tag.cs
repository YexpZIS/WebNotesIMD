using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Tags
{
    public enum Tag
    {
        Header,
        ListItem,
        Code,
        InlineCode,
        Image,
        ImageCarousel,
        Download,
        Audio,
        Video,
        Text
    }

    public abstract class ITag
    {
        public int id { get; set; }
        public abstract Dictionary<Tag, string[]> tags { get; set; }

        public int GetNextId()
        {
            id++;
            return id;
        }

        public void ResetId()
        {
            id = 0;
        }
    }
}
