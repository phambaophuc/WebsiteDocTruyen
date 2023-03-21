using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteDocTruyen.Models
{
    public class Story
    {
        [Key]
        public int StoryID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
    }
}