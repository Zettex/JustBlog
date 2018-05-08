using JustBlog.Core;
using JustBlog.Core.Objects;
using JustBlog.Models;
using JustBlog.Providers;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JustBlog.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountProvider _accountProvider;
        private readonly IBlogRepository _blogRepository;
        
        char DirSeparator = Path.DirectorySeparatorChar;

        public AccountController(IAccountProvider authProvider, IBlogRepository blogRepository = null)
        {
            _accountProvider = authProvider;
            _blogRepository = blogRepository;
        }

        /// <summary>
        /// Return the login page.
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            // If already logged-in redirect the user to manage page.
            if (_accountProvider.IsLoggedIn)
                return RedirectToUrl(returnUrl);

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        /// <summary>
        /// Login POST action.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && _accountProvider.Login(_blogRepository, model.UserName, model.Password))
            {
                // Session - мега костыль, но посути все из за нхибернейта......
                var user = _blogRepository.User(model.UserName);
                Session["role"] = user.Role;
                Session["userId"] = user.Id;
                Session["username"] = user.Nickname;
                Session["email"] = user.Email;

                model.Message = "Вход воспроизведен";
                model.RedirectUrl = Url.Action("Posts", "Blog");
                return View(model);
            }

            ModelState.AddModelError("", "Неверное имя пользователя или пароль");
            return View(model);
        }


        // Add new user
        [AllowAnonymous]
        public ActionResult Register()
        {
            // If already logged-in redirect the user to home page.
            if (_accountProvider.IsLoggedIn)
                return RedirectToAction("Posts", "Blog");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (ValidString(model.UserName))
                {
                    if (_blogRepository.UserNameExist(model.UserName))
                    {
                        ModelState.AddModelError("", "Указанное имя пользователя уже используется в системе");
                    }
                    else if (_blogRepository.UserEmailExist(model.Email))
                    {
                        ModelState.AddModelError("", "Указанный email уже используется в системе");
                    }
                    else
                    {
                        CreateDir(_blogRepository.Register(model.UserName, model.Email, model.Password));
                        model.Message = "Регистрация прошла успешно";
                        model.RedirectUrl = Url.Action("Login", "Account");
                        return View(model);
                    
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Некорректное имя пользователя");
                }
            }

            return View(model);
        }

        /// <summary>
        /// Logout the user and return the login page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            _accountProvider.Logout();

            return RedirectToAction("Login", "Account");
        }

        public new ActionResult User(int userId)
        {
            User model = _blogRepository.User(userId);
            if (model == null)
                throw new HttpException(404, "Пользователь не найден");

            if (_accountProvider.IsLoggedIn && (int)Session["userId"] == userId)
            {
                model.CanEdit = true;

                if (TempData["msg"] != null)
                {
                    model.Message = TempData["msg"].ToString();
                    TempData["msg"] = null;
                }

                return View(model);
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public new ActionResult User(User model)
        {
            if (ModelState.IsValid)
            {
                if (ValidString(model.Nickname))
                {

                    if (Session["username"].ToString() != model.Nickname)
                    {
                        if (_blogRepository.UserNameExist(model.Nickname))
                            ModelState.AddModelError("", "Указанное имя пользователя уже используется в системе");
                    }
                    else if (Session["email"].ToString() != model.Email)
                    {
                        if (_blogRepository.UserEmailExist(model.Email))
                            ModelState.AddModelError("", "Указанный email уже используется в системе");
                    }
                    else
                    {
                        var userId = (int)Session["userId"];
                        _blogRepository.EditUser(model, userId);
                        if (model.Message == null)
                            model.Message = "Данные были изменены";
                        if (_accountProvider.IsLoggedIn && (int)Session["userId"] == userId)
                            model.CanEdit = true;

                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Некорректное имя пользователя");
                }
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AvatarUpload(UserAvatarModel model, int userId)
        {
            UploadFile(model.File);
            
            TempData["msg"] = "Аватар успешно загружен";

            return RedirectToAction("User", "Account", new { userId });

            //User(model, "Аватар успешно загружен");
        }

        /// <summary>
        /// Child action that returns the user avatar as partial view.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult UserAvatar(int userId)
        {
            UserAvatarModel model = new UserAvatarModel { UserId = userId };

            string avatarPath = "~\\Content\\Users\\" + userId.ToString() + "\\Avatar\\";
            string avatarFullPath = System.Web.HttpContext.Current.Server.MapPath(avatarPath);

            model.ImgPath = avatarPath + new DirectoryInfo(avatarFullPath).GetFiles()
                          .Select(fi => fi.Name)
                          .FirstOrDefault(name => name != "Thumbs.db");
            
            if (_accountProvider.IsLoggedIn && (int)Session["userId"] == userId)
            {
                model.CanEdit = true;
            }

            return PartialView("_UserAvatar", model);
        }

        private string UploadFile(HttpPostedFileBase file)
        {
            char DirSeparator = Path.DirectorySeparatorChar;
            string FilesPath = System.Web.HttpContext.Current.Server.MapPath("~\\Content" + DirSeparator + "Users" + DirSeparator + Session["userId"].ToString() + DirSeparator + "Avatar" + DirSeparator);
            
            // Check if we have a file
            if (null == file) return "";
            // Make sure the file has content
            if (!(file.ContentLength > 0)) return "";

            string fileName =  WordGenerator6();
            Session["avatarName"] = fileName;
            string fileExt = Path.GetExtension(file.FileName);

            // Make sure we were able to determine a proper extension
            if (null == fileExt) return "";

            // Check if the directory we are saving to exists
            if (!Directory.Exists(FilesPath))
            {
                // If it doesn't exist, create the directory
                Directory.CreateDirectory(FilesPath);
            }

            ClearDir(FilesPath);

            // Set our full path for saving
            string path = FilesPath + DirSeparator + fileName;
            
            System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);

            // Save the image in PNG format.
            img.Save(Path.GetFullPath(path + ".png"), System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();
            
            // Save our thumbnail as well
            //ResizeImage(model.File, 70, 70);

            // Return the filename
            return fileName;
        }

        private void ClearDir(string path)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

        private void CreateDir(int userId)
        {
            string FilePath = System.Web.HttpContext.Current.Server.MapPath("~\\Content" + DirSeparator + "Users" + DirSeparator + userId + DirSeparator + "Avatar" + DirSeparator);
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
                System.Drawing.Image img = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("~\\Content\\images\\placeholder.png"));
                img.Save(Path.GetFullPath(FilePath + "placeholder.png"), System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private string WordGenerator6()
        {
            Random rnd = new Random();
            string letters = "abcdefjgklmnoprstuwxyz1234567890ABCDEFJGKLMNOPRSTUWXYZ";
            string word = "";

            for (int i = 0; i < 6; i++)
            {
                int num = rnd.Next(0, letters.Length); 
                word += letters[num];
            }

            return word;
        }

        private ActionResult RedirectToUrl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                if (Session["role"].ToString() == "Admin")
                    return RedirectToAction("Manage", "Admin");
                else
                    return RedirectToAction("Posts", "Blog");
            }
        }

        private bool ValidString(string str)
        {
            return str.Trim() != "";
        }

        /// <summary>
        /// Child action that returns the user sidebar partial view.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult UserSidebar(int userId)
        {
            User model = _blogRepository.User(userId);
            
            return PartialView("_UserSidebar", model);
        }
    }
}
