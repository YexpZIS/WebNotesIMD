using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.HtmlObjects
{
    interface IHtmlObject
    {
        string isHtmlObject(string[] Data, int index, int depth);
        int GetIndex();
    }
}
