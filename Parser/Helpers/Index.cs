using System.Linq;

namespace Parser.Helpers
{
    public class Index
    {
        private IDepth _depth;

        public Index(ref IDepth depth)
        {
            _depth = depth;
        }

        public int FindIndex(ref string[] lines, int index, int nowDepth, string delimiter = "----")
        {
            return lines.Skip(index).TakeWhile(x => x.IndexOf(delimiter) == -1 || _depth.GetDepth(x) != nowDepth).Count() + index;
        }
    }
}
