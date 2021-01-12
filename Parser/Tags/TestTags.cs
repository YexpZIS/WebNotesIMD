using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Tags
{
    public class TestTags : ITag
    {
        public override Dictionary<Tag, string[]> tags { get; set; } = new Dictionary<Tag, string[]>()
        {
            { Tag.BookTitle, new string[]{ "<bookName>{0}</bookName>"} },
            // {0} - Title, {2} - book name, {1} - path/to page
            { Tag.Summary, new string[]{ "<folder id={0}>{1}<buttons>{2}</buttons></folder>", "<button url={2}/{1}>{0}</button>" } },

            { Tag.Header, new string[]{ "<head>{0}</head>\n", "<tags>{0}</tags>\n"  } },
            { Tag.ListItem,new string[]{ "<card-head id={0}>{1}</card-head>\n", "<text id={0}>{1}</text>\n" } },
            { Tag.Code, new string[]{ "<code>{0}</code>\n" } },
            { Tag.InlineCode, new string[]{ "<inline-code>{0}</inline-code>" } },
            { Tag.Image, new string[]{ "<img>{0}</img>\n"} },
            { Tag.ImageCarousel, new string[]{ "<carousel>\n<ol>{0}</ol>\n<div>{1}</div>\n</carousel>\n", "<li data-context-to='{0}'></li>","<img>{0}</img>" } },//replase carusel id
            { Tag.Download, new string[]{ "<download name={0}>{1}</download>" } },
            { Tag.Audio, new string[]{ "<audio name={0}>{1}</audio>\n" } },
            { Tag.Video, new string[]{ "<video type={0}>{1}</video>\n" } },
            { Tag.Text, new string[]{ "{0}"} },
        };

    }
}
