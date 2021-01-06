using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Helpers
{
    public class Seeker
    {
        private Index _index;
        private IDepth _depth;

        public Seeker()
        {
            _depth = new Depth();
            _index = new Index(ref _depth);
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
    }
}
