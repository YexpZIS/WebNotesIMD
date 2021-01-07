using Parser.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.HtmlObjects
{
    public class InlineCode : IHtmlObject
    {
        private ITag _tag;

        private int index;
        private string delimitor = "````";

        public InlineCode(ITag tag)
        {
            _tag = tag;
        }

        public int GetIndex()
        {
            return index;
        }

        public string isHtmlObject(string[] Data, int index, int depth)
        {
            this.index = index;

            

            return test(Data[index]);
        }

        // вынести в класс с Helpers т.к. могут быть другие разделители: **text** , __text__
        public string method(string line)
        {
            // переписать в цикл

            int endIndex = line.IndexOf(delimitor);
            if (endIndex != -1) 
            {
                // Before code
                var beforeCode = line.Substring(0, endIndex);

                line = line.Substring(beforeCode.Length + delimitor.Length, line.Length - delimitor.Length - beforeCode.Length);

                // ICode
                endIndex = line.IndexOf(delimitor);
                var inlineCode = line.Substring(0, endIndex);

                // AfterCode
                line = line.Substring(inlineCode.Length + delimitor.Length, line.Length - delimitor.Length - inlineCode.Length);


                return beforeCode + string.Format(_tag.tags[Tag.InlineCode][0], inlineCode) + line;
            }
            return null;
        }

        // вынести в класс с Helpers т.к. могут быть другие разделители: **text** , __text__
        // сократить дублирование
        public string test(string line)
        {
            string result = "";
            string code = "";
            int lenght = 0;

            while ((index = line.IndexOf(delimitor))!= -1)
            {
                result += line.Substring(0, index);

                lenght = index + delimitor.Length;
                line = line.Substring(lenght, line.Length - lenght);

                index = line.IndexOf(delimitor);
                code = line.Substring(0, index);
                result += string.Format(_tag.tags[Tag.InlineCode][0], code);

                lenght = index + delimitor.Length;
                line = line.Substring(lenght, line.Length - lenght);
            }

            result += line;

            return result;
        }
    }
}
