//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ACME_INC_POE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_PURCHASES
    {
        public string CUST_USERNAME { get; set; }
        public int PRODUCT_ID { get; set; }
        public System.DateTime PURCHASE_DATE { get; set; }
        public System.DateTime DELIVERY_DATE { get; set; }
    
        public virtual TBL_CUSTOMER TBL_CUSTOMER { get; set; }
        public virtual TBL_PRODUCTS TBL_PRODUCTS { get; set; }
    }
}
