using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Login_Modal.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="*")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Pwd { get; set; }
    }
}