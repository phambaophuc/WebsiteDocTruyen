using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteDocTruyen.Models
{
    public class History
    {
        [Key]
        public int HistoryID { get; set; }

        public int ChapterID { get; set; }
        public virtual Chapter Chapter { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }

        public DateTime DateRead { get; set; }
    }

}