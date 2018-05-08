
using System.ComponentModel.DataAnnotations;

namespace JustBlog.Core.Objects
{
    /// <summary>
    /// Encapsulates the information submitted by the contact form.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// The user name.
        /// </summary>
        [Required(ErrorMessage = "Требуется имя отправителя")]
        [Display(Name = "Имя отправителя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Требуется email"), EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Url]
        [Display(Name = "Веб-сайт")]
        public string Website { get; set; }

        [Required(ErrorMessage = "Требуется тема")]
        [Display(Name = "Тема")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Требуется тело сообщения")]
        [Display(Name = "Сообщение")]
        public string Body { get; set; }
    }
}
