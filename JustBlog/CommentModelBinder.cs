#region Usings
using JustBlog.Core;
using JustBlog.Core.Objects;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
#endregion

namespace JustBlog
{
    public class CommentModelBinder : DefaultModelBinder
    {
        private readonly IKernel _kernel;

        public CommentModelBinder(IKernel kernel)
        {
            _kernel = kernel;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var blogRepository = _kernel.Get<IBlogRepository>();

            var comment = new Comment();
            
            // Edit comment
            if (bindingContext.ValueProvider.GetValue("Id").AttemptedValue != "0")
            {
                var commentId = Int32.Parse(bindingContext.ValueProvider.GetValue("Id").AttemptedValue);
                comment = blogRepository.Comment(commentId);
                comment.Content = bindingContext.ValueProvider.GetValue("Content").AttemptedValue;

                return comment;
            }
            // Add comment
            else
            {
                comment.Id = 0;
                comment.Content = bindingContext.ValueProvider.GetValue("Content").AttemptedValue;
                var dateTime = DateTime.Parse(bindingContext.ValueProvider.GetValue("DateSent").AttemptedValue);
                comment.DateSent = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                var postId = Int32.Parse(bindingContext.ValueProvider.GetValue("Post").AttemptedValue);
                comment.Post = blogRepository.Post(postId);
                comment.Deleted = false;

                if (bindingContext.ValueProvider.GetValue("Owner").AttemptedValue != "")
                {
                    var ownerId = Int32.Parse(bindingContext.ValueProvider.GetValue("Owner").AttemptedValue);
                    var owner = blogRepository.Comment(ownerId);
                    comment.Owner = owner;
                }

                return comment;
            }
        }
    }
}