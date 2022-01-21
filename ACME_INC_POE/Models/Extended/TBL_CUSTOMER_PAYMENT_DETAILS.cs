using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACME_INC_POE.Models
{
    [MetadataType(typeof(PayMetaData))]
    public partial class TBL_CUSTOMER_PAYMENT_DETAILS
    {
        
    }

    public class PayMetaData
    {
        //add validation
        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email required.")]
        public string CUST_USERNAME { get; set; }

        [Display(Name = "Card Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*Required")]
        public int CUST_CARD_NUMBER { get; set; }

        [Display(Name = "CVV")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*Required.")]
        public int CUST_CVV { get; set; }
        public virtual TBL_CUSTOMER TBL_CUSTOMER { get; set; }
    }

    
}