using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteDocTruyen.ViewModels
{
    public class ReadViewModel
    {
        public int ChapterID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int StoryID { get; set; }
        public string StoryTitle { get; set; }
        public int NextChapterID { get; set; }
        public int PreviousChapterID { get; set; }
    }
}