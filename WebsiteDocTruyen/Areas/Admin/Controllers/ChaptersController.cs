using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDocTruyen.Models;
using WebsiteDocTruyen.ViewModels;

namespace WebsiteDocTruyen.Areas.Admin.Controllers
{
    public class ChaptersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public ChaptersController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET: Admin/Chapters
        public ActionResult Create(int id, ChapterViewModel model)
        {
            var story = _dbContext.Stories.Find(id);
            ViewBag.ChapterTitle = story.Title;

            if (ModelState.IsValid)
            {
                var chapter = new Chapter{
                   Title = model.Title,
                   Content = model.Content,
                   StoryID = story.StoryID,
                };
                _dbContext.Chapters.Add(chapter);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Stories");
            }

            model.Story = _dbContext.Stories.ToList();
            return View(model);
        }

    }
}