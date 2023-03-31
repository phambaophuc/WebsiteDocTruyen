using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebsiteDocTruyen.Models
{
    public class Chapter
    {
        [Key]
        public int ChapterID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime TimeUpdate { get; set; }
        public int StoryID { get; set; }
        public virtual Story Story { get; set; }
    }
}