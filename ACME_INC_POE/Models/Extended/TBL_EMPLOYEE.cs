using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACME_INC_POE.Models
{
    [MetadataType(typeof(EmpMetaData))]
    public partial class TBL_EMPLOYEE
    {
        public string ConfirmPassword { get; set; }
    }

    public class EmpMetaData
    {
        //add validation
        [Display(Name = "Email Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username required.")]
        public string EMP_USERNAME { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required.")]
        [MinLength(8, ErrorMessage = "Minimum 6 characters allowed.")]
        public string EMP_PASSWORD { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("EMP_PASSWORD", ErrorMessage = "Passwords do not match.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required.")]
        [MinLength(8, ErrorMessage = "Minimum 6 characters allowed.")]
        public string ConfirmPassword { get; set; }
    }
}