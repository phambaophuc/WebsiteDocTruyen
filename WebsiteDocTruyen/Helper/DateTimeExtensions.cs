using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteDocTruyen.Helper
{
    public static class DateTimeExtensions
    {
        public static string ToRelativeDateString(this DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan.TotalSeconds < 60)
            {
                return "vừa xong";
            }
            else if (timeSpan.TotalMinutes < 60)
            {
                var minutes = (int)timeSpan.TotalMinutes;
                return $"{minutes} {(minutes > 1 ? "phút" : "phút")} trước";
            }
            else if (timeSpan.TotalHours < 24)
            {
                var hours = (int)timeSpan.TotalHours;
                return $"{hours} {(hours > 1 ? "giờ" : "giờ")} trước";
            }
            else if (timeSpan.TotalDays < 7)
            {
                var days = (int)timeSpan.TotalDays;
                return $"{days} {(days > 1 ? "ngày" : "ngày")} trước";
            }
            else
            {
                return dateTime.ToString("dd/MM/yyyy");
            }
        }

    }
}