using Parser.Helpers;
using Parser.HtmlObjects;
using Parser.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using Parser.Disassemble;

namespace Parser.TableOfContents
{
    public class FolderItem : IHtmlObject
    {
        private IDisassemble _disassemble;

        private string symbol = "- ";
        private int depth = 0;

        private string Name = "";
        private string[] Body;

        public FolderItem(ITag tag, Seeker seeker, SummaryDisassemble disassemble) : base(tag, seeker)
        {
            _disassemble = disassemble;
        }

        public void SetDisassembler(IDisassemble disassemble)
        {
            _disassemble = disassemble;
        }

        public override string isHtmlObject(string[] Data, int index, int depth)
        {
            this.depth = _seeker.GetDepth(Data[index]);
            string folder = _seeker.RemovePrefix(Data[index], depth);

            if (folder.StartsWith(symbol))
            {
                Name = ParseName(folder);

                index++;
                int folderEnd = _seeker.FindIndexOnThisDepth(ref Data, index, this.depth, "- ");
                if (folderEnd != -1) 
                {
                    Body = Data.Skip(index).Take(folderEnd - index).ToArray();
                    this.index = folderEnd - 1;
                }

                return GetHtml();
            }

            return null;
        }

        private string ParseName(string folder)
        {
            return folder.Substring(symbol.Length);
        }

        private string GetHtml()
        {
            return string.Format(_tags.tags[Tag.Summary][0], _tags.GetNextId(),
                Name, _disassemble.Disassemble(Body, depth + 1));
        }
    }
}
