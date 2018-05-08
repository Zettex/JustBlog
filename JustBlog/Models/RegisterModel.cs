using System.ComponentModel.DataAnnotations;

namespace JustBlog.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Требуется имя пользователя")]
        [Display(Name = "Имя пользователя")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Имя пользователя не может быть больше 15 и меньше 3 символов")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Требуется email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Требуется пароль")]
        [Display(Name = "Пароль")]
        [MinLength(6, ErrorMessage = "Пароль не может быть меньше 6 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Требуется подтверждение пароля")]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        public string Message { get; set; }

        public string RedirectUrl { get; set; }
    }
}