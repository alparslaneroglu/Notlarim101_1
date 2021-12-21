using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Notlarim101.Entity.ValueObject
{
    public class LoginViewModel
    {
        [DisplayName("Kullanici Adi"), Required(ErrorMessage ="{0} alani boş geçilemez."),StringLength(30,ErrorMessage ="{0} max. {1} karakter olmali")]
        public string UserName { get; set; }
        [DisplayName("Sifre"), Required(ErrorMessage = "{0} alani boş geçilemez."),DataType(DataType.Password),StringLength(30, ErrorMessage = "{0} max. {1} karakter olmali")]
        public string Password { get; set; }

    }
}