using Parser.Helpers;
using Parser.HtmlObjects;
using Parser.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Parser.TableOfContents
{
    public class FolderItem : IHtmlObject
    {
        private string symbol = "- ";

        private string Name = "";

        public FolderItem(ITag tag, Seeker seeker) : base(tag, seeker)
        {

        }

        public override string isHtmlObject(string[] Data, int index, int depth)
        {
            string folder = _seeker.RemovePrefix(Data[index], depth);

            if (folder.StartsWith(symbol))
            {
                Name = ParseName(folder);
            }

            return null;
        }

        private string ParseName(string folder)
        {
            return "";
        }
    }
}
