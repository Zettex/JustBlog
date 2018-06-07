#region Usings
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace JustBlog.Core.Objects
{
    /// <summary>
    /// Represents a tag that is labelled on a post.
    /// </summary>
    public class Tag
    {
        public virtual int Id
        { get; set; }

        [Required(ErrorMessage = "Название: Поле обязательно для заполнения")]
        [StringLength(500, ErrorMessage = "Название: Длина не должна превышать 500 символов")]
        public virtual string Name
        { get; set; }

        [Required(ErrorMessage = "UrlSlug: Поле обязательно для заполнения")]
        [StringLength(500, ErrorMessage = "UrlSlug: Длина не должна превышать 500 символов")]
        public virtual string UrlSlug
        { get; set; }

        public virtual string Description
        { get; set; }

        [JsonIgnore]
        public virtual IList<Post> Posts
        { get; set; }
    }
}
