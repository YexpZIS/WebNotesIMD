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

        private int codeLenght = 0;

        public Code(ITag tag, Seeker seeker, Text text)
        {
            _tag = tag;
            _seeker = seeker;
            _text = text;
        }

        public int GetIndex()
        {
            return index;
        }

        public string isHtmlObject(string[] Data, int index, int depth)
        {
            code = null;
            codeLenght = 0;

            for (int i = index; i < Data.Length; i++) 
            {
                _depth = _seeker.GetDepth(Data[i]);
                if (_depth > depth)
                {
                    code += _text.isHtmlObject(Data, i, depth + 1);
                    codeLenght++;
                }
                else
                {
                    this.index = index + codeLenght - 1;

                    break;
                }
            }

            this.index = codeLenght == 0 ? index : index + codeLenght - 1;


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
