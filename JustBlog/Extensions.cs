using JustBlog.Core.Objects;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace JustBlog
{
    public static class Extensions
    {
        static TimeZoneInfo TZInfo = TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["Timezone"]);

        /// <summary>
        /// Convert the passed datetime from UTC timezone to configured timezone in web.config.
        /// </summary>
        /// <param name="utcDT"></param>
        /// <returns></returns>
        public static string ToConfigLocalTime(this DateTime utcDT)
        {
            return String.Format("{0}", TimeZoneInfo.ConvertTimeFromUtc(utcDT, TZInfo).ToShortDateString());
        }

        public static string GetLocalCommentDateSent(this DateTime utcDT)
        {
            var convertedDate = TimeZoneInfo.ConvertTimeFromUtc(utcDT, TZInfo);

            return convertedDate.ToString("dd MMMM, yyyy @ HH:mm");
        }

        public static string Href(this Post post, UrlHelper helper)
        {
            return helper.RouteUrl(new
            {
                controller = "Blog",
                action = "Post",
                year = post.PostedOn.Year,
                month = post.PostedOn.Month,
                title = post.UrlSlug
            });
        }
    }
}