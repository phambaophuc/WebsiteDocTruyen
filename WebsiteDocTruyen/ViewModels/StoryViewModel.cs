using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebsiteDocTruyen.Models;

namespace WebsiteDocTruyen.ViewModels
{
    public class StoryViewModel
    {
        public int StoryID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Description { get; set; }
        [Required]
        public string Img { get; set; }
        public List<Genre> Genres { get; set; }
        public List<int> SelectedGenres { get; set; }
    }
}