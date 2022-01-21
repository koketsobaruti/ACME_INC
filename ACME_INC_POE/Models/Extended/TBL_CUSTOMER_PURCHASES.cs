using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACME_INC_POE.Models
{
    [MetadataType(typeof(CustPurchasesMetaData))]
    public partial class TBL_CUSTOMER_PURCHASES
    {

    }

    public class CustPurchasesMetaData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CUST_USERNAME { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PRODUCT_ID { get; set; }
        public string PURCHASE_DATE { get; set; }

    }
}