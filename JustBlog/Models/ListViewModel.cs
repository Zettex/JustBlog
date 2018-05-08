#region Usings
using JustBlog.Core;
using JustBlog.Core.Objects;
using PagedList;
#endregion

namespace JustBlog.Models
{
    /// <summary>
    /// View model used for list view.
    /// </summary>
    /// <remarks>
    /// Same view model is used to list posts for category, tag or search text.
    /// </remarks>
    public class ListViewModel
    {
        public ListViewModel(IBlogRepository blogRepository, int p)
        {
            TotalPosts = blogRepository.TotalPosts();
            Posts = blogRepository.Posts().ToPagedList(p, 5);
        }

        public ListViewModel(IBlogRepository blogRepository, string text, string type, int p)
        {
            switch (type)
            {
                case "Category":
                    Posts = blogRepository.PostsForCategory(text).ToPagedList(p, 5);
                    TotalPosts = blogRepository.TotalPostsForCategory(text);
                    Category = blogRepository.Category(text);
                    break;

                case "Tag":
                    Posts = blogRepository.PostsForTag(text).ToPagedList(p, 5);
                    TotalPosts = blogRepository.TotalPostsForTag(text);
                    Tag = blogRepository.Tag(text);
                    break;

                default:
                    Posts = blogRepository.PostsForSearch(text).ToPagedList(p, 5);
                    TotalPosts = blogRepository.TotalPostsForSearch(text);
                    Search = text;
                    break;
            }
        }

        public IPagedList<Post> Posts
        { get; private set; }

        public int TotalPosts
        { get; private set; }

        public Category Category
        { get; private set; }

        public Tag Tag
        { get; private set; }

        public string Search
        { get; private set; }
    }
}