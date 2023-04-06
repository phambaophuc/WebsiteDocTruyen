using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDocTruyen.Models;
using System.Data.Entity;
using PagedList;

namespace WebsiteDocTruyen.Controllers
{
    public class StoriesController : Controller
    {
        public readonly ApplicationDbContext _dbContext;
        public StoriesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // Chức năng tìm truyện theo tên
        public ActionResult SearchStory(int? page, string searchString)
        {
            if (page == null) page = 1;
            int pageSize = 8;
            int pageNum = page ?? 1;

            var stories = _dbContext.Stories.Where(s => s.Chapters.Any()).Include(s => s.Genres);

            if (!String.IsNullOrEmpty(searchString))
            {
                stories = stories.Where(s => s.Title.Contains(searchString));
            }

            ViewBag.Result = searchString;

            return View(stories.ToList().ToPagedList(pageNum, pageSize));
        }
        public JsonResult GetSuggestions(string term)
        {
            var titles = _dbContext.Stories
                .Where(s => s.Title.Contains(term))
                .Select(s => s.Title)
                .ToList();

            return Json(titles, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchStoryForGenre(int? page, int genreID = 0)
        {
            if (page == null) page = 1;
            int pageSize = 8;
            int pageNum = page ?? 1;

            var stories = _dbContext.Stories.Where(s => s.Chapters.Any()).Include(s => s.Genres);

            if (genreID != 0)
            {
                stories = stories.Where(s => s.Genres.Any(x => x.GenreID == genreID));
                ViewBag.Genre = _dbContext.Genres.FirstOrDefault(g => g.GenreID == genreID);
            }

            var genres = _dbContext.Genres.ToList();
            var model = new SelectList(genres, "GenreID", "Name", genreID);
            ViewBag.GenreID = model;

            return View(stories.ToList().ToPagedList(pageNum, pageSize));
        }

        // Hiển thị truyện có chapter mới nhất
        public ActionResult LatestChapterStories(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 8;
            int pageNum = page ?? 1;

            var latestChapterStories = _dbContext.Stories
                .Where(s => s.Chapters.Any())
                .OrderByDescending(s => s.Chapters.Max(c => c.TimeUpdate))
                .ToList();
            return View(latestChapterStories.ToPagedList(pageNum, pageSize));
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