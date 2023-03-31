using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDocTruyen.Models;
using System.Data.Entity;

namespace WebsiteDocTruyen.Controllers
{
    public class StoriesController : Controller
    {
        public readonly ApplicationDbContext _dbContext;
        public StoriesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // Hiển thị truyện có chapter mới nhất
        public ActionResult LatestChapterStories()
        {
            var latestChapterStories = _dbContext.Stories
                .OrderByDescending(s => s.Chapters.Max(c => c.DateTime))
                .ToList();

            return View(latestChapterStories);
        }

        // Thông tin truyện
        public ActionResult Details(int id)
        {
            var story = _dbContext.Stories.Include(s => s.Genres).FirstOrDefault(s => s.StoryID == id);

            if (story == null)
            {
                return HttpNotFound();
            }

            return View(story);
        }

    }
}