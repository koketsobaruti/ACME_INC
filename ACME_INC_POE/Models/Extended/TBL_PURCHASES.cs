using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ACME_INC_POE.Models
{
    [MetadataType(typeof(PurchMetaData))]
    public partial class TBL_PURCHASES
    {

    }

    public class PurchMetaData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CUST_USERNAME { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PRODUCT_ID { get; set; }
        public DateTime PURCHASE_DATE { get; set; }
        public DateTime DELIVERY_DATE { get; set; }

        public virtual TBL_CUSTOMER TBL_CUSTOMER { get; set; }
        public virtual TBL_PRODUCTS TBL_PRODUCTS { get; set; }
    }
}