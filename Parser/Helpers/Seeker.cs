using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Helpers
{
    public class Seeker
    {
        private Index _index;
        private IDepth _depth;
        private LineModifier _modifier;

        public Seeker(LineModifier modifier, IDepth depth, Index index)
        {
            _modifier = modifier;
            _depth = depth;
            _index = index;
        }

        // Index

        public int FindIndex(ref string[] lines, int index, int nowDepth, string delimiter = "----")
        {
            return _index.FindIndex(ref lines, index, nowDepth, delimiter);
        }

        // Depth

        public int GetDepth(string line)
        {
            return _depth.GetDepth(line);
        }
        public string RemovePrefix(string line, int depth)
        {
            return _depth.RemoveTabsAndSpacesBeforeText(line, depth);
        }

        // LineModifier
        public string InsertTagsInLine(string line, string tags, string delimiter = "````")
        {
            return _modifier.InsertTagsInLine(line, tags, delimiter);
        }
    }
}
