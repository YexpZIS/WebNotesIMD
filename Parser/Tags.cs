using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    public enum Tags
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

    public interface ITags
    {
        int id { get; set; }
        Dictionary<Tags, string[]> tags { get; set; }
    }

    public class TestTags : ITags
    {
        public int id { get; set; } = 0;

        public Dictionary<Tags, string[]> tags { get; set; } = new Dictionary<Tags, string[]>()
        {
            { Tags.Header, new string[]{ "<head>{0}</head>\n", "<tags>{0}</tags>\n"  } },
            { Tags.ListItem,new string[]{ "<card-head id={0}>{1}</card-head>\n", "<text id={0}>{1}</text>\n" } },
            { Tags.Code, new string[]{ "<code>{0}</code>\n" } },
            { Tags.InlineCode, new string[]{ "<inline-code>{0}</inline-code>\n" } },
            { Tags.Image, new string[]{ "<img>{0}</img>\n"} },
            { Tags.ImageCarousel, new string[]{ "<carousel>\n<ol>{0}</ol>\n<div>{1}</div>\n</carousel>\n", "<li data-context-to='{0}'></li>","<img>{0}</img>" } },//replase carusel id
            { Tags.Download, new string[]{ "<download name={0}>{1}</download>" } },
            { Tags.Audio, new string[]{ "<audio name={0}>{1}</audio>\n" } },
            { Tags.Video, new string[]{ "<video type={0}>{1}</video>\n" } },
            { Tags.Text, new string[]{ "{0}\n"} },
        };
    }

    public class WebNotes : ITags
    {
        public int id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Dictionary<Tags, string[]> tags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
