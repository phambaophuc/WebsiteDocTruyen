using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebsiteDocTruyen.Models;
using WebsiteDocTruyen.ViewModels;

namespace WebsiteDocTruyen.Areas.Admin.Controllers
{
    public class StoriesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public StoriesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        public ActionResult Details(int id)
        {
            var story = _dbContext.Stories.Include(s => s.Genres).FirstOrDefault(s => s.StoryID == id);

            if (story == null)
            {
                return HttpNotFound();
            }
            int chapterCount = story.ChapterNumber;

            ViewBag.ChapterCount = chapterCount;

            return View(story);
        }
        [Authorize]
        public ActionResult Create()
        {
            var model = new StoryViewModel
            {
                Genres = _dbContext.Genres.ToList()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _dbContext.Genres.ToList();
                return View(viewModel);
            }

            var story = new Story
            {
                Title = viewModel.Title,
                Author = viewModel.Author,
                Description = viewModel.Description,
                Img = viewModel.Img,
                ChapterNumber = 0,
                DateTime = DateTime.Now,
                Genres = new List<Genre>()
            };

            foreach (var genreId in viewModel.SelectedGenres)
            {
                var genre = _dbContext.Genres.Single(g => g.GenreID == genreId);
                story.Genres.Add(genre);
            }

            _dbContext.Stories.Add(story);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Index(string searchString, int genreID = 0)
        {
            ViewBag.Keyword = searchString;

            var stories = _dbContext.Stories.Include(s => s.Genres);

            if (!String.IsNullOrEmpty(searchString))
            {
                stories = stories.Where(s => s.Title.Contains(searchString));
            }
            if (genreID != 0)
            {
                stories = stories.Where(s => s.Genres.Any(x => x.GenreID == genreID));
            }

            ViewBag.GenreID = new SelectList(_dbContext.Genres, "GenreID", "Name");
            return View(stories.ToList());
        }

        public ActionResult Edit(int id)
        {
            var story = _dbContext.Stories
                .Include(s => s.Genres)
                .Single(s => s.StoryID == id);

            if (story == null)
            {
                return HttpNotFound();
            }

            var viewModel = new StoryViewModel
            {
                StoryID = story.StoryID,
                Title = story.Title,
                Author = story.Author,
                Description = story.Description,
                Img = story.Img,
                SelectedGenres = story.Genres.Select(g => g.GenreID).ToList(),
                Genres = _dbContext.Genres.ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _dbContext.Genres.ToList();
                return View(viewModel);
            }

            var story = _dbContext.Stories
                .Include(s => s.Genres)
                .Single(s => s.StoryID == viewModel.StoryID);

            story.Title = viewModel.Title;
            story.Author = viewModel.Author;
            story.Description = viewModel.Description;
            story.Img = viewModel.Img;
            story.Genres.Clear();

            foreach (var genreId in viewModel.SelectedGenres)
            {
                var genre = _dbContext.Genres.Single(g => g.GenreID == genreId);
                story.Genres.Add(genre);
            }

            _dbContext.SaveChanges();

            return RedirectToAction("Details", "Stories", new { id = story.StoryID });
        }

        public ActionResult Delete(int id)
        {
            var story = _dbContext.Stories.Find(id);

            if (story == null)
            {
                return HttpNotFound();
            }

            _dbContext.Stories.Remove(story);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/Images/" + file.FileName));
            return "/Content/Images/" + file.FileName;
        }
    }
}