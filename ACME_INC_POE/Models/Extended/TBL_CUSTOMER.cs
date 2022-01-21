using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ACME_INC_POE.Models
{
    [MetadataType(typeof(CustMetaData))]
    public partial class TBL_CUSTOMER
    {
        public string ConfirmPassword { get; set; }
    }

    public class CustMetaData
    {
        //add validation
        [Display(Name = "Email Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username required.")]
        public string CUST_USERNAME { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required.")]
        [MinLength(8, ErrorMessage = "Minimum 8 characters allowed.")]
        public string CUST_PASSWORD { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("CUST_PASSWORD", ErrorMessage = "Passwords do not match.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required.")]
        [MinLength(8, ErrorMessage = "Minimum 8 characters allowed.")]
        public string ConfirmPassword { get; set; }
    }
}