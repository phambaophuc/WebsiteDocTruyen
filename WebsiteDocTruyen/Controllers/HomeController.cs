using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDocTruyen.Models;
using System.Data.Entity;
using WebsiteDocTruyen.ViewModels;
using System.IO;
using System.Threading.Tasks;
using PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.Ajax.Utilities;

namespace WebsiteDocTruyen.Controllers
{
    public class HomeController : Controller
    {
        public readonly ApplicationDbContext _dbContext;
        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public ActionResult Index(int? page, string searchString, int genreID = 0)
        {
            if (page == null) page = 1;
            int pageSize = 12;
            int pageNum = page ?? 1;

            ViewBag.Keyword = searchString;

            var stories = _dbContext.Stories.Where(s => s.Chapters.Any()).Include(s => s.Genres);

            if (!String.IsNullOrEmpty(searchString))
            {
                stories = stories.Where(s => s.Title.Contains(searchString));
            }
            if (genreID != 0)
            {
                stories = stories.Where(s => s.Genres.Any(x => x.GenreID == genreID));
            }

            ViewBag.GenreID = new SelectList(_dbContext.Genres, "GenreID", "Name");

            return View(stories.ToList().ToPagedList(pageNum, pageSize));
        }

        // Phương thức đọc truyện
        public ActionResult Read(int storyId, int chapterId)
        {
            var chapter = _dbContext.Chapters.Include(c => c.Story).FirstOrDefault(c => c.ChapterID == chapterId);

            if (chapter == null || chapter.StoryID != storyId)
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = new ReadViewModel()
            {
                ChapterID = chapter.ChapterID,
                Title = chapter.Title,
                Content = chapter.Content,
                StoryID = chapter.StoryID,
                StoryTitle = chapter.Story.Title,
                NextChapterID = _dbContext.Chapters
                    .Where(c => c.StoryID == storyId && c.ChapterID > chapterId)
                    .OrderBy(c => c.ChapterID)
                    .Select(c => c.ChapterID)
                    .FirstOrDefault(),
                PreviousChapterID = _dbContext.Chapters
                    .Where(c => c.StoryID == storyId && c.ChapterID < chapterId)
                    .OrderByDescending(c => c.ChapterID)
                    .Select(c => c.ChapterID)
                    .FirstOrDefault()
            };

            UpdateHistory(chapterId);

            return View(viewModel);
        }

        // Lưu vào history khi đọc truyện
        public void UpdateHistory(int chapterId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var existingHistory = _dbContext.History.FirstOrDefault(h => h.ChapterID == chapterId && h.UserID == userId);

                if (existingHistory != null)
                {
                    existingHistory.DateRead = DateTime.Now;
                    _dbContext.SaveChanges();
                }
                else
                {
                    var newHistory = new History
                    {
                        ChapterID = chapterId,
                        DateRead = DateTime.Now,
                        UserID = userId
                    };
                    _dbContext.History.Add(newHistory);
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}