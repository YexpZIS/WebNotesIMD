using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotes.Controllers
{
    public class PageController : Controller
    {
        private readonly MarkdownParser _parser;

        public PageController(MarkdownParser parser)
        {
            _parser = parser;
        }

        public ActionResult Index()
        {
            return View("TableOfContents");
        }

        public ActionResult TestPage()
        {
            ViewBag.Html = _parser.ParsePage("index.md");
            return View("Content");
        }

        [HttpGet]
        public ContentResult Get(string page)
        {
            if (page == null)
            {
                return base.Content("Unknown page","text/html");
            }

            

            return base.Content(_parser.ParsePage(page), "text/html");
        }


    }
}
