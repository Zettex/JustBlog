using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustBlog.Models
{
    public class UserAvatarModel
    {
        public int UserId { get; set; }

        public string ImgPath { get; set; }

        public HttpPostedFileBase File { get; set; }

        public virtual bool CanEdit { get; set; }
    }
}