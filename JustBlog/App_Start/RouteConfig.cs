using System.Web.Mvc;
using System.Web.Routing;

namespace JustBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              "Post",
              "Archive/{year}/{month}/{title}",
              new { controller = "Blog", action = "Post" }
            );

            routes.MapRoute(
              "Archive",
              "Archive/{year}/{month}",
              new { controller = "Blog", action = "Archive", year = UrlParameter.Optional, month = UrlParameter.Optional }
            );

            routes.MapRoute(
              "Category",
              "Category/{category}",
              new { controller = "Blog", action = "Category" }
            );

            routes.MapRoute(
              "Tag",
              "Tag/{tag}",
              new { controller = "Blog", action = "Tag" }
            );

            routes.MapRoute(
              "Login",
              "Login",
              new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
              "Logout",
              "Logout",
              new { controller = "Account", action = "Logout" }
            );
            
            routes.MapRoute(
              "UserPage",
              "User/{userId}",
              new { controller = "Account", action = "User" }
            );

            routes.MapRoute(
              "AvatarUpload",
              "AvatarUpload/{userId}",
              new { controller = "Account", action = "AvatarUpload" }
            );

            routes.MapRoute(
              "UserAvatar",
              "UserAvatar/{userId}",
              new { controller = "Account", action = "UserAvatar" }
            );

            routes.MapRoute(
              "UserSidebar",
              "UserSidebar/{userId}",
              new { controller = "Account", action = "UserSidebar" }
            );

            routes.MapRoute(
             "AboutBlog",
             "AboutBlog",
             new { controller = "Blog", action = "AboutBlog" }
           );

            routes.MapRoute(
              "Manage",
              "Manage",
              new { controller = "Admin", action = "Manage" }
            );

            routes.MapRoute(
              "AdminAction",
              "Admin/{action}",
              new { controller = "Admin", action = "Login" }
            );

            routes.MapRoute(
             "Register",
             "Register",
             new { controller = "Account", action = "Register" }
           );

            routes.MapRoute(
              "Action",
              "{action}",
              new { controller = "Blog", action = "Posts" }
            );
        }
    }
}