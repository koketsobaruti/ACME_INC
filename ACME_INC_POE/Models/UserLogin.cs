using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ACME_INC_POE.Models
{
    
    public class UserLogin
    {
        [Key]
        [Display(Name = "Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Usename required.")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required.")]
        [MinLength(8, ErrorMessage = "Minimum 6 characters allowed.")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

    }
}