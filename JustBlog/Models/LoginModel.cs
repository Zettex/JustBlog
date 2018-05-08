using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace JustBlog.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Требуется имя пользователя")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Требуется пароль")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        
        public string Message { get; set; }
        
        public string RedirectUrl { get; set; }
    }
}