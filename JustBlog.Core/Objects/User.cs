using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace JustBlog.Core.Objects
{
    public class User
    {
        public virtual int Id
        { get; set; }

        [Required(ErrorMessage = "Требуется имя пользователя")]
        [Display(Name = "Имя пользователя")]
        [MaxLength(15)]
        [MinLength(3)]
        public virtual string Nickname
        { get; set; }
        
        [Display(Name = "Пароль")]
        public virtual string Password
        { get; set; }

        [Required(ErrorMessage = "Требуется email")]
        [Display(Name = "Email")]
        public virtual string Email
        { get; set; }
        
        [Display(Name = "ФИО")]
        public virtual string FullName
        { get; set; }

        [Display(Name = "Пол")]
        public virtual string Gender
        { get; set; }

        [Display(Name = "Адрес")]
        public virtual string Address
        { get; set; }

        [Display(Name = "Сайт")]
        public virtual string Site
        { get; set; }

        [Display(Name = "О себе")]
        public virtual string AboutMe
        { get; set; }

        public virtual DateTime RegisterDate
        { get; set; }

        public virtual string Role
        { get; set; }
        
        [JsonIgnore]
        public virtual IList<Comment> Comments
        { get; set; }


        public virtual string Message { get; set; }

        public virtual bool CanEdit { get; set; }

        public virtual HttpPostedFileBase File { get; set; }
    }
}
