using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustBlog.Core.Objects
{
    public class Comment
    {
        public virtual int Id
        { get; set; }

        public virtual DateTime DateSent
        { get; set; }

        public virtual string Content
        { get; set; }

        public virtual Comment Owner
        { get; set; }

        public virtual User User
        { get; set; }

        public virtual Post Post
        { get; set; }

        public virtual bool Deleted
        { get; set; }
    }
}
