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

    public interface ITag
    {
        int id { get; set; }
        Dictionary<Tag, string[]> tags { get; set; }

        int GetNextId();
    }
}
