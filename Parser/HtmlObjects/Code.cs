using Parser.Helpers;
using Parser.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.HtmlObjects
{
    public class Code : IHtmlObject
    {
        private ITag _tag;
        private Seeker _seeker;

        private string code;
        private int index;

        private int _depth;

        private List<string> tmpCode;

        public Code(ITag tag, Seeker seeker)
        {
            _tag = tag;
            _seeker = seeker;
            tmpCode = new List<string>();
        }

        public int GetIndex()
        {
            return index;
        }

        public string isHtmlObject(string[] Data, int index, int depth)
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

            return string.Format(_tag.tags[Tag.Code][0], code);
        }
    }
}
