using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ACME_INC_POE.Models
{
    [MetadataType(typeof(AddressMetaData))]
    public partial class TBL_CUST_ADDRESSES
    {
        public string BuildingName { get; set; }
        public bool Default { get; set; }
    }

    public class AddressMetaData
    {
        //add validation
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email required.")]
        public string CUST_USERNAME { get; set; }

        [Display(Name = "*Type your full street address: ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address required.")]
        public string CUST_ADDRESS { get; set; }

        [Display(Name = "Apartment, Building, Floor and/or Company Name:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address required.")]
        public string BuildingName { get; set; }

        [Display(Name = "Make this my default delivery address")]
        public bool Default { get; set; }
        public virtual TBL_CUSTOMER TBL_CUSTOMER { get; set; }
    }
}