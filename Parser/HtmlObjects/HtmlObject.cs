using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.HtmlObjects
{
    interface HtmlObject
    {
        string isHtmlObject(ref string[] Data, int index);
        int GetIndex();
    }
}
