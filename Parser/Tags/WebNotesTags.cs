using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Tags
{
    class WebNotesTags : ITag
    {
        public override Dictionary<Tag, string[]> tags { get; set; } = new Dictionary<Tag, string[]>()
        {
            { Tag.BookTitle, new string[]{ @"<div class='text-center book-name'>
                                                <label>{0}</label>
                                                <img src='img/cross.svg' onclick='closeNav()'/>
                                            </div>
                        <div class='summary mt-3'>
                            <div id='accordion'>" } },
            { Tag.Summary, new string[]{ @"<div class='card'>
                                <div class='card-header' >
                                    <h5 class='mb-0'>
                                        <button class='btn btn-link button-text-fix' data-toggle='collapse' data-target='#block_menu_{0}' aria-expanded='false'>
                                            {1}
                                        </button>
                                    </h5>
                                </div>
                                <div id='block_menu_{0}' class='collapse show' aria-labelledby='block_menu_{0}' data-parent='#accordion_menu_{0}'>
                                    <div class='card-body bg-light'>{2}</div></div></div>",
                "<div class='page'><button class=\"button-text-fix\" onclick=\"load('{2}','{1}');\">{0}</button></div>" } },

            { Tag.Header, new string[]{ @"<div class='cus_body'>
                    <div class='container-fluid'>
                        <h1 class='text-center'>{0}</h1>
                    </div>", @"<div class='search-tags'>
                        {0}
                    </div>" } },
            { Tag.ListItem,new string[]{ @"
                    <div class='card'>
                        <div class='card-header' >
                            <h5 class='mb-0'>
                                <button class='select btn btn-link button-text-fix text-dark' data-toggle='collapse' data-target='#block{0}' aria-expanded='false'>
                                    {1}
                                </button>
                            </h5>
                        </div>",
                                   @"<div id='block{0}' class='collapse show' aria-labelledby='block{0}' data-parent='#accordion{0}'>
                            <div class='card-body'>
                                {1}
                            </div>
                        </div>
                    </div>" } },
            { Tag.Code, new string[]{ @"<div class='code'><pre>{0}</pre></div>" } },
            { Tag.InlineCode, new string[]{ "<div class='inline-code'>{0}</div>" } },
            /*{ Tag.Image, new string[]{ "<img>{0}</img>\n"} },
            { Tag.ImageCarousel, new string[]{ "<carousel>\n<ol>{0}</ol>\n<div>{1}</div>\n</carousel>\n", "<li data-context-to='{0}'></li>","<img>{0}</img>" } },//replase carusel id
            { Tag.Download, new string[]{ "<download name={0}>{1}</download>" } },
            { Tag.Audio, new string[]{ "<audio name={0}>{1}</audio>\n" } },
            { Tag.Video, new string[]{ "<video type={0}>{1}</video>\n" } },*/
            { Tag.Text, new string[]{ "{0}"} },
        };

    }
}
