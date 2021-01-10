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

        public ActionResult Index(string book ,string page = "index.md")
        {
            ViewBag.Book = book;
            ViewBag.Page = page;
            return View("TableOfContents");
        }

        /*public ActionResult TestPage()
        {
            ViewBag.Html = _parser.ParsePage("index.md");
            return View("Content");
        }*/

        [HttpGet]
        public ActionResult Get(string book, string page = "index.md")
        {
            ViewBag.Html = _parser.ParsePage($"{book}/{page}");
            return View("Content");
        }

        /*[HttpGet]
        public ContentResult Get(string page = "index.md")
        {
            base.Content("<h1>Tag</h1>text", "text/html");
        }*/

     }
}
