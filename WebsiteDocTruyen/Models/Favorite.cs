using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteDocTruyen.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public int StoryId { get; set; }
        public virtual Story Story { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}