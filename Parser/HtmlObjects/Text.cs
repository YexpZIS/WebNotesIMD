using Parser.Helpers;
using Parser.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser.HtmlObjects
{
    public class Text : IHtmlObject
    {
        private ITag _tag;
        private Seeker _seeker;

        private int index;
        private string separator = "<br>\n";

        public Text(ITag tag, Seeker seeker)
        {
            _tag = tag;
            _seeker = seeker;
        }

        public int GetIndex()
        {
            return index;
        }

        public string isHtmlObject(string[] Data, int index, int depth)
        {
            this.index = index;

            string clearText = _seeker.RemovePrefix(Data[index], depth);

            /*if (clearText == "" && 
                !isTextEmpty(ref Data, index-1, depth) && 
                !isTextEmpty(ref Data, index + 1, depth)) 
            {
                return "<br>\n<br>\n";
            }*/
            /*if (clearText == "")
            {
                if (!isTextEmpty(ref Data, index - 2, depth) &&
                    !isTextEmpty(ref Data, index + 1, depth))
                {
                    return Separator(2);
                }
                else if (!isTextEmpty(ref Data, index - 1, depth) &&
                    !isTextEmpty(ref Data, index + 1, depth))
                {
                    return Separator(1);
                }
            }*/

            var b = Data.Take(index).Count(x => _seeker.RemovePrefix(x, depth) != "");
            var a = Data.Skip(index).Count(x => _seeker.RemovePrefix(x, depth) != "");

            if (clearText == "" && a != 0 && b != 0)
            {
                return separator;
            }

            /*if (clearText == "" && !isTextEmpty(ref Data, index - 1, depth) &&
                    !isTextEmpty(ref Data, Data.Length - 1, depth))
            {
                for (int i = index; i < Data.Length; i++)
                {
                    if (_seeker.RemovePrefix(Data[i], depth) == "")
                    {
                        text += separator;
                    }
                    else
                    {
                        this.index = i - 1;
                        break;
                    }
                }


                return text;
            }*/
            else
            {
                return string.Format(_tag.tags[Tag.Text][0], clearText);
            }
        }

        private string Separator(int quantity)
        {
            string text = "";

            for (int i = 0; i < quantity; i++)
            {
                text += separator;
            }

            return text;
        }

        private bool isTextEmpty(ref string[] Data, int index, int depth)
        {
            if (Data.Length > index && index >= 0)
            {
                if (_seeker.RemovePrefix(Data[index], depth) != "") 
                {
                    return false;
                }
            }

            return true;
        }
    }
}
