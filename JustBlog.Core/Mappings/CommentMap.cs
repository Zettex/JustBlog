using FluentNHibernate.Mapping;
using JustBlog.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustBlog.Core.Mappings
{
    public class CommentMap : ClassMap<Comment>
    {
        public CommentMap()
        {
            Id(x => x.Id);
            Map(x => x.DateSent).Not.Nullable();
            Map(x => x.Content).Length(500).Not.Nullable();
            Map(x => x.Deleted).Not.Nullable();
            References(x => x.Owner).Column("Owner");
            References(x => x.User).Column("`User`").Not.Nullable();
            References(x => x.Post).Column("Post").Not.Nullable();
        }
    }
}
