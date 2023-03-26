using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteDocTruyen.ViewModels
{
    public class HistoryViewModel
    {
        public int HistoryID { get; set; }
        public int StoryID { get; set; }
        public string StoryTitle { get; set; }
        public string StoryImg { get; set; }
        public int ChapterID { get; set; }
        public string ChapterTitle { get; set; }
        public DateTime DateRead { get; set; }
        public List<HistoryViewModel> HistoryItems { get; set; }
    }
}