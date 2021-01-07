﻿using Parser.HtmlObjects;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Parser.Disassemble
{
    public class BodyDisassemble : IDisassemble
    {
        private IServiceProvider _service;

        private List<IHtmlObject> htmlObjects = new List<IHtmlObject>();

        private string[] _body;
        private string finalString = "";

        public BodyDisassemble(IServiceProvider service)
        {
            _service = service;
        }

        public string Disassemble(string[] body, int nowDepth)
        {
            InitHtmlObjects();
            _body = body;

            for (int i = 0; i < body.Length; i++)
            {
                findHtmlObject(ref i);
            }
            return finalString;
        }

        private void InitHtmlObjects()
        {
            htmlObjects.Add(_service.GetService<ListItem>());
            htmlObjects.Add(_service.GetService<Text>());
        }

        private void findHtmlObject(ref int index)
        {
            foreach (var obj in htmlObjects)
            {
                var text = obj.isHtmlObject(_body, index);

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
