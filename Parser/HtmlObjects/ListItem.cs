using Parser.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser.HtmlObjects
{
    class ListItem : HtmlObject
    {
        private Dictionary<Tags, string[]> _tags;
        private IBodyDisassemble _body;

        private int nowDepth = 0;
        // выделить в отдельный класс
        // GeneralData
        private int id;
        private Seeker _seeker;
        /*public void SetGeneralData(ref GData gd)
        {
            _gd=gd;
        }*/

        public IBodyDisassemble GetBodyDisassemble()
        {
            // for tests
            if (_body == null)
            {
                //_body = new BodyDisassemble(null,null);
            }

            return _body;
        }
        public void SetBodyDisassemble(IBodyDisassemble body)
        {
            _body = body;
        }

        public ListItem(ref Dictionary<Tags, string[]> tags, int id)
        {
            _tags = tags;
            this.id = id;
            _seeker = new Seeker();
        }

        public string isHtmlObject(ref string[] Data, int index)
        {
            if (Data.Length > index + 1 && Data[index + 1].IndexOf("----") != -1)
            {
                string head = Data[index];
                // save block header depth 
                nowDepth = _seeker.GetDepth(head);

                string[] body = GetBody(ref Data, index + 2);
                //index = end - 1;

                head = _seeker.RemovePrefix(head, nowDepth);
                var Body = GetBodyDisassemble();//new BodyDisassemble(ref _tags, ref body);
                Body.SetData(ref body);
                string b = Body.Disassemble(nowDepth);
                //body = _seeker.RemovePrefix(body,nowDepth);

                return string.Format(_tags[Tags.ListItem][0], id, head) +
                    string.Format(_tags[Tags.ListItem][1], id, b);
            }

            return null;
        }
        private string[] GetBody(ref string[] Data, int index)
        {
            int end = _seeker.FindIndex(ref Data, index, nowDepth);
            return Data.Skip(index).Take(end - index).ToArray();
        }

        public int GetIndex()
        {
            return 0;
        }


        public string GetHtml(ref string[] Data)
        {
            // remove tabs and spaces


            // return string.Format(head.Pattern, head.Data) + string.Format(body.Pattern, body.Disassemble(depth));
            return null;
        }
    }
}
