using Parser.Disassemble;
using Parser.Helpers;
using Parser.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser.HtmlObjects
{
    public class ListItem : IHtmlObject
    {
        private IDisassemble _body;

        private int nowDepth = 0;
        private string[] Data;

        public ListItem(IDisassemble disassemble,ITag tags, Seeker seeker) : base(tags, seeker)
        {
            _body = disassemble;
        }

        public override string isHtmlObject(string[] Data, int index, int depth)
        {
            this.index = index;
            this.Data = Data;

            if (hasNextLine() && isNextLineHas())
            {
                saveDepth();

                string head = _seeker.RemovePrefix(Data[index], nowDepth); 
                string[] body = GetBody();

                string bodyStr = _body.Disassemble(body, nowDepth);

                return GetHtml(head, bodyStr);
            }

            return null;
        }

        private bool hasNextLine()
        {
            return Data.Length > index + 1;
        }
        private bool isNextLineHas(string delimiter = "----")
        {
            return Data[index + 1].IndexOf("----") != -1;
        }

        private void saveDepth()
        {
            nowDepth = _seeker.GetDepth(Data[index]);
        }


        private string[] GetBody()
        {
            index += 2; // Skip header and delimiter
            int end = _seeker.FindIndex(ref Data, index, nowDepth);
            var body =  Data.Skip(index).Take(end - index - 1).ToArray();
            index = end - 2;
            return body;
        }

        public string GetHtml(string head, string body)
        {
            return string.Format(_tags.tags[Tag.ListItem][0], _tags.GetNextId(), head) +
                    string.Format(_tags.tags[Tag.ListItem][1], _tags.id , body); 
        }
    }
}
