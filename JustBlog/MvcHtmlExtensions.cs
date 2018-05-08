
using JustBlog.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace JustBlog
{
    public static class MvcHtmlExtensions
    {
        public static MvcHtmlString PostLink(this HtmlHelper helper, Post post)
        {
            return helper.ActionLink(post.Title, "Post", "Blog", new { year = post.PostedOn.Year, month = post.PostedOn.Month, title = post.UrlSlug }, new { title = post.Title });
        }

        public static MvcHtmlString CategoryLink(this HtmlHelper helper, Category category)
        {
            return helper.ActionLink(category.Name, "Category", "Blog", new { category = category.UrlSlug }, new { title = String.Format("Все посты с категоией {0}", category.Name) });
        }

        public static MvcHtmlString TagLink(this HtmlHelper helper, Tag tag)
        {
            return helper.ActionLink(tag.Name, "Tag", "Blog", new { tag = tag.UrlSlug }, new { title = String.Format("Все посты с тегом {0}", tag.Name) });
        }

        public static MvcHtmlString UserLink(this HtmlHelper helper, string userId)
        {
            return helper.ActionLink("Профиль пользователя", "User", "Account", new { userId }, null );
        }

        public static MvcHtmlString TextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, bool editable, object htmlAttributes = null)
        {
            if (htmlAttributes == null)
            {
                var dict = new Dictionary<string, object>();
                if (!editable)
                {
                    dict.Add("readonly", "readonly");
                }
                IDictionary<string, object> attrs = new RouteValueDictionary(dict);

                return System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, attrs);
            }
            else
            {
                var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                if (!editable)
                {
                    attrs.Add("readonly", "readonly");
                }

                return System.Web.Mvc.Html.TextAreaExtensions.TextAreaFor(htmlHelper, expression, attrs);
            }
        }

        public static MvcHtmlString TextAreaFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, bool editable, object htmlAttributes = null)
        {
            if (htmlAttributes == null)
            {
                var dict = new Dictionary<string, object>();
                if (!editable)
                {
                    dict.Add("readonly", "readonly");
                }
                IDictionary<string, object> attrs = new RouteValueDictionary(dict);

                return System.Web.Mvc.Html.TextAreaExtensions.TextAreaFor(htmlHelper, expression, attrs);
            }
            else
            {
                var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                if (!editable)
                {
                    attrs.Add("readonly", "readonly");
                }

                return System.Web.Mvc.Html.TextAreaExtensions.TextAreaFor(htmlHelper, expression, attrs);
            }
        }

        public static MvcHtmlString DropDownListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList,
            string option, bool editable, object htmlAttributes = null)
        {
            if (htmlAttributes == null)
            {
                var dict = new Dictionary<string, object>();
                if (!editable)
                {
                    dict.Add("disabled", "disabled");
                }
                IDictionary<string, object> attrs = new RouteValueDictionary(dict);

                return System.Web.Mvc.Html.SelectExtensions.DropDownListFor(htmlHelper, expression, selectList, option, attrs);
            }
            else
            {
                var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                if (!editable)
                {
                    attrs.Add("disabled", "disabled");
                }

                return System.Web.Mvc.Html.SelectExtensions.DropDownListFor(htmlHelper, expression, selectList, option, attrs);
            }
            
        }
    }
}