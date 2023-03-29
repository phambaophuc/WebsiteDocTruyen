using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebsiteDocTruyen.Models;

namespace WebsiteDocTruyen.ViewModels
{
    public class ChapterViewModel
    {
        public int ChapterID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public int StoryID { get; set; }
        public IEnumerable<Story> Story { get; set; }
    }
}