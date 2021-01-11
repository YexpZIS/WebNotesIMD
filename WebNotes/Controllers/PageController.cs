using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace WebNotes.Controllers
{
    public class PageController : Controller
    {
        private readonly IConfiguration _config;
        private readonly MarkdownParser _parser;
        //private string pathToSource = "bin/Debug/netcoreapp3.1/source";

        private FileInfo file;

        public PageController(IConfiguration config, MarkdownParser parser)
        {
            _config = config;
            _parser = parser;
        }

        public ActionResult Index(string book ,string page = "index.md")
        {
            ViewBag.Book = book;
            ViewBag.Page = page;
            return View("TableOfContents");
        }

        [HttpGet]
        // <img class="img-fluid" src="https://localhost:44372/Page/Image?book=TestBook&path=source/vim.jpg"/>
        public IActionResult Image(string book, string path) 
        {
            // Works with types: jpg, png, bmp, gif and other

            file = new FileInfo(GetFullPath(book, path));

            if (file.Exists)
            {
                var b = System.IO.File.ReadAllBytes(file.FullName);
                string info = "image/" + file.Extension.Replace(".", "");
                return File(b, info);
            }
            else
            {
                return base.Content("Image not found.", "text/html");
            }
        }

        [Route("Page/Media")]
        // https://localhost:44372/Page/Video?book=TestBook&path=source/v.mp4
        public FileResult VideoAndAudio(string book, string path)
        {
            file = new FileInfo(GetFullPath(book, path));

            if (file.Exists)
            {
                return PhysicalFile(file.FullName, "application/octet-stream", enableRangeProcessing: true);
            }

            return null;
        }

        private string GetFullPath(string book, string path)
        {
            return $"{_config["SourcePath"]}/{book}/{path}";
        }

        /*public ActionResult TestPage()
        {
            ViewBag.Html = _parser.ParsePage("index.md");
            return View("Content");
        }*/

        [HttpGet]
        public ActionResult Get(string book, string page = "index.md")
        {
            ViewBag.Html = _parser.ParsePage(GetFullPath(book, page));
            return View("Content");
        }

        /*[HttpGet]
        public ContentResult Get(string page = "index.md")
        {
            base.Content("<h1>Tag</h1>text", "text/html");
        }*/

     }
}
