using System.Collections.Generic;
using System.Linq;

namespace Parser.Helpers
{
    enum Symbols
    {
        Tab,
        Space
    }
    public interface IDepth
    {
        int GetDepth(string line);
        string RemoveTabsAndSpacesBeforeText(string line, int depth);
    }
    public class Depth : IDepth
    {
        private Dictionary<Symbols, float> _weight = new Dictionary<Symbols, float>
        {
            { Symbols.Tab , 1f},
            { Symbols.Space, 0.25f}
        };

        public int GetDepth(string line)
        {
            float depth = 0;

            var prefix = line.TakeWhile(x => x == ' ' || x == '\t');

            depth = prefix.Where(x => x == '\t').Count() * _weight[Symbols.Tab];
            depth += prefix.Where(x => x == ' ').Count() * _weight[Symbols.Space];

            return (int)depth;
        }

        public string RemoveTabsAndSpacesBeforeText(string line, int depth)
        {
            float nowDepth = 0;

            for (int i = 0; i < line.Length; i++)
            {
                if ((int)nowDepth == depth)
                {
                    return new string(line.Skip(i).ToArray());
                }

                if (line[i].Equals('\t'))
                {
                    nowDepth += _weight[Symbols.Tab];
                }
                else if (line[i].Equals(' '))
                {
                    nowDepth += _weight[Symbols.Space];
                }

            }

            return line;
        }
    }
}
