using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Helpers
{
    public class LineModifier
    {
        private string result = "";
        private string text = "";
        private string delimiter = "````";

        private int index = 0;
        private int length = 0;

        /// <param name="tags">_tag.tags[Tag.InlineCode][0] or "<inline-code>{0}</inline-code>"</param>
        public string InsertTagsInLine(string line, string tags, string delimiter = "````")
        {
            ClearValues();

            this.delimiter = delimiter;

            while ((index = findIndex(line)) != -1)
            {
                // Text before delimiter
                result += getText(line);
                line = RemoveTextAndDelimiter(line);

                // Text between delimiters
                index = findIndex(line);
                text = getText(line);
                result += getHtml(tags);
                line = RemoveTextAndDelimiter(line);
            }

            result += line;

            return result;
        }
        private string getHtml(string tags)
        {
            return string.Format(tags, text);
        }

        private string getText(string line)
        {
            if (index == -1)
            {
                return line;
            }
            return line.Substring(0, index);
        }

        private int findIndex(string line)
        {
            return line.IndexOf(delimiter);
        }

        private string RemoveTextAndDelimiter(string text)
        {
            if (index == -1)
            {
                return "";
            }
            else
            {
                length = index + delimiter.Length;
                return text.Substring(length, text.Length - length);
            }
        }

        private void ClearValues()
        {
            result = "";
            text = "";
            delimiter = "````";

            index = 0;
            length = 0;
        }
    }
}
