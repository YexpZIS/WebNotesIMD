﻿using Parser.Helpers;
using Parser.HtmlObjects;
using Parser.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.TableOfContents
{
    public class FolderItem : IHtmlObject
    {
        public FolderItem(ITag tag, Seeker seeker) : base(tag, seeker)
        {

        }

        public override string isHtmlObject(string[] Data, int index, int depth)
        {
            throw new NotImplementedException();
        }
    }
}
