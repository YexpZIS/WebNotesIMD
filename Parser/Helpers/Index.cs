using System.Linq;

namespace Parser.Helpers
{
    public class Index
    {
        private IDepth _depth;

        public Index(IDepth depth)
        {
            _depth = depth;
        }

        public int FindIndex(ref string[] lines, int index, int nowDepth, string delimiter = "----")
        {
            int result = lines.Skip(index).TakeWhile(x => x.IndexOf(delimiter) == -1 || _depth.GetDepth(x) != nowDepth).Count() + index;
            
            if (result == lines.Length)
            {
                result++;
            }

            return result;
        }

        public int FindIndexOnThisDepth(ref string[] lines, int index, int nowDepth, string delimiter = "----")
        {
            for (int i = index; i < lines.Length; i++)
            {
                if (lines[i].IndexOf(delimiter) != -1 && _depth.GetDepth(lines[i]) == nowDepth)
                {
                    return i;
                }
                else if (_depth.GetDepth(lines[i]) < nowDepth + 1)
                {
                    return i;
                }
            }

            return lines.Length;
        }
    }
}
