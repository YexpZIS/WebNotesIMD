using Parser.Helpers;
using Parser.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.HtmlObjects
{
    public class Code : IHtmlObject
    {
        private string code;
        private int _depth;
        private List<string> tmpCode;

        public Code(ITag tags, Seeker seeker) : base(tags, seeker)
        {
            tmpCode = new List<string>();
        }

        public override string isHtmlObject(string[] Data, int index, int depth)
        {
            ClearLocalValues();

            for (int i = index; i < Data.Length; i++) 
            {
                _depth = _seeker.GetDepth(Data[i]);
                if (_depth > depth)
                {
                    tmpCode.Add(_seeker.RemovePrefix(Data[i], depth + 1));
                }
                else
                {
                    break;
                }
            }

            code = string.Join("\n", tmpCode);
            this.index = index + (tmpCode.Count == 0 ? 0 : tmpCode.Count - 1);


            return GetHtml(code);
        }
        
        private void ClearLocalValues()
        {
            code = null;
            tmpCode.Clear();
        }

        public string GetHtml(string code)
        {
            if (tmpCode.Count == 0)
            {
                return null;
            }

            return string.Format(_tags.tags[Tag.Code][0], code);
        }
    }
}
