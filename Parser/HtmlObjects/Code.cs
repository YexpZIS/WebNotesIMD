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
        private Text _text;

        private string code;
        private int index;

        private int _depth;

        public Code(ITag tag, Seeker seeker, Text text)
        {
            _tag = tag;
            _seeker = seeker;
            _text = text;
        }

        public int GetIndex()
        {
            return index - 1;
        }

        public string isHtmlObject(string[] Data, int index, int depth)
        {
            code = null;

            for (int i = index; i < Data.Length; i++) 
            {
                _depth = _seeker.GetDepth(Data[i]);
                if (_seeker.GetDepth(Data[i]) > depth)
                {
                    code += _text.isHtmlObject(Data, i, depth + 1);
                }
                else
                {
                    this.index = i;
                    break;
                }
            }
            
            return GetHtml(code);
        }

        public string GetHtml(string code)
        {
            if (code == null)
            {
                return null;
            }

            return string.Format(_tag.tags[Tag.Code][0], code);
        }
    }
}
