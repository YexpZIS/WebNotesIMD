using Parser.HtmlObjects;
using System;
using System.Collections.Generic;

namespace Parser.Disassemble
{
    public interface IDisassemble
    {
        string Disassemble(string[] body, int nowDepth);
    }

    public abstract class AbsDisassemble : IDisassemble
    {
        protected IServiceProvider _provider;

        protected List<IHtmlObject> htmlObjects = new List<IHtmlObject>();

        protected string[] _body;
        protected string finalString = "";
        protected int depth = 0;

        protected AbsDisassemble(IServiceProvider service)
        {
            _provider = service;
        }

        public string Disassemble(string[] body, int nowDepth)
        {
            _body = body;
            depth = nowDepth;

            InitHtmlObjects();
            finalString = "";

            for (int i = 0; i < body.Length; i++)
            {
                findHtmlObject(ref i);
            }
            return finalString;
        }

        protected abstract void InitHtmlObjects();

        private void findHtmlObject(ref int index)
        {
            foreach (var obj in htmlObjects)
            {
                var text = obj.isHtmlObject(_body, index, depth);

                if (text != null)
                {
                    index = obj.GetIndex();
                    finalString += text;
                    break;
                }
            }
        }
    }
}