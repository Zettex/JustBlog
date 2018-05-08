
using JustBlog.Core;
using System.Web;
using System.Web.Security;

namespace JustBlog.Providers
{
    public class AccountProvider : IAccountProvider
    {
        /// <summary>
        /// Return True if the user is already logged-in.
        /// </summary>
        public bool IsLoggedIn
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        /// <summary>
        /// Authenticate an user and set cookie if user is valid.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(IBlogRepository blogRepository, string username, string password)
        {
            string result = blogRepository.Login(username, password); // TODO: User Membership APIs

            if (result != null)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }

            return result != null;
        }

        /// <summary>
        /// Logout the user.
        /// </summary>
        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}