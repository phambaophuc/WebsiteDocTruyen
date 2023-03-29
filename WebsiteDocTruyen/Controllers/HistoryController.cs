using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDocTruyen.Models;
using WebsiteDocTruyen.ViewModels;
using System.Data.Entity;

namespace WebsiteDocTruyen.Controllers
{
    public class HistoryController : Controller
    {
        public readonly ApplicationDbContext _dbContext;
        public HistoryController()
        {
            _dbContext = new ApplicationDbContext();
        }
        
        // hiện lịch sử đọc
        [Authorize]
        public ActionResult ReadingHistory()
        {
            var userId = User.Identity.GetUserId();

            var history = _dbContext.History.Include(h => h.Chapter.Story)
                                            .Where(h => h.UserID == userId)
                                            .GroupBy(h => h.Chapter.StoryID)
                                            .Select(g => g.OrderByDescending(h => h.DateRead).FirstOrDefault())
                                            .ToList();

            var viewModel = new HistoryViewModel()
            {
                HistoryItems = history.Select(h => new HistoryViewModel()
                {
                    StoryID = h.Chapter.StoryID,
                    StoryTitle = h.Chapter.Story.Title,
                    StoryImg = h.Chapter.Story.Img,
                    ChapterID = h.ChapterID,
                    ChapterTitle = h.Chapter.Title,
                    DateRead = h.DateRead
                }).ToList()
            };

            return View(viewModel);
        }

        // Xoá truyện cần xoá trong lịch sử
        public ActionResult DeleteHistory(int id)
        {
            var userId = User.Identity.GetUserId();

            _dbContext.History.Where(h => h.UserID == userId && h.Chapter.StoryID == id).ToList().ForEach(p => _dbContext.History.Remove(p));
            _dbContext.SaveChanges();

            return RedirectToAction("ReadingHistory");
        }
        // Xoá tất cả truyện trong lịch sử
        public ActionResult DeleteAllHistory()
        {
            var userId = User.Identity.GetUserId();
            _dbContext.History.Where(h => h.UserID == userId).ToList().ForEach(p => _dbContext.History.Remove(p));
            _dbContext.SaveChanges();
            return RedirectToAction("ReadingHistory");
        }
    }
}