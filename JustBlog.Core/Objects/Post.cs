#region Usings
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace JustBlog.Core.Objects
{
    /// <summary>
    /// Represents a blog entry - article, presentation or any thing.
    /// </summary>
    public class Post
    {
        [Required(ErrorMessage = "Id: Field is required")]
        public virtual int Id
        { get; set; }

        /// <summary>
        /// The heading of the post.
        /// </summary>
        [Required(ErrorMessage = "Заголовок: Поле обязательно для заполнения")]
        [StringLength(500, ErrorMessage = "Заголовок: Длина не должна превышать 500 символов")]
        public virtual string Title
        { get; set; }

        /// <summary>
        /// A brief paragraph about the post.
        /// </summary>
        [Required(ErrorMessage = "Краткое описание: Поле обязательно для заполнения")]
        public virtual string ShortDescription
        { get; set; }

        /// <summary>
        /// The complete post content.
        /// </summary>
        [Required(ErrorMessage = "Описание: Поле обязательно для заполнения")]
        public virtual string Description
        { get; set; }

        /// <summary>
        /// The information about the post that has to be displayed in the &lt;meta&gt; tag (SEO).
        /// </summary>
        /// <remarks>
        /// Not sure Google still uses this for ranking but other search providers might be.
        /// </remarks>
        [Required(ErrorMessage = "Meta: Поле обязательно для заполнения")]
        [StringLength(1000, ErrorMessage = "Meta: Длина не должна превышать 500 символов")]
        public virtual string Meta
        { get; set; }

        /// <summary>
        /// The url slug that is used to define the post address.
        /// </summary>
        [Required(ErrorMessage = "UrlSlug: Поле обязательно для заполнения")]
        [StringLength(1000, ErrorMessage = "UrlSlug: Поле не должно превышать 50 символов")]
        public virtual string UrlSlug
        { get; set; }

        /// <summary>
        /// Flag to represent whether the article is published or not.
        /// </summary>
        public virtual bool Published
        { get; set; }

        /// <summary>
        /// The post published date.
        /// </summary>
        [Required(ErrorMessage = "PostedOn: Field is required")]
        public virtual DateTime PostedOn
        { get; set; }

        /// <summary>
        /// The post's last modified date.
        /// </summary>
        public virtual DateTime? Modified
        { get; set; }

        /// <summary>
        /// The category to which the post belongs to.
        /// </summary>
        public virtual Category Category
        { get; set; }

        /// <summary>
        /// Collection of tags labelled over the post.
        /// </summary>
        public virtual IList<Tag> Tags
        { get; set; }

        //public virtual User User
        //{ get; set; }

        [JsonIgnore]
        public virtual IList<Comment> Comments
        { get; set; }
    }
}
