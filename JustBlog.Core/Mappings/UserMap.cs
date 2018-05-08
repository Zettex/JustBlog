using FluentNHibernate.Mapping;
using JustBlog.Core.Objects;

namespace JustBlog.Core.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.Nickname).Length(50).Not.Nullable();
            Map(x => x.Password).Length(50).Not.Nullable();
            Map(x => x.Email).Not.Nullable();
            Map(x => x.FullName).Length(200);
            Map(x => x.Gender);
            Map(x => x.Address).Length(250);
            Map(x => x.Site).Length(250);
            Map(x => x.AboutMe);
            Map(x => x.RegisterDate).Not.Nullable();
            Map(x => x.Role).Not.Nullable();
            HasMany(x => x.Comments).KeyColumn("`User`");
        }
    }
}
