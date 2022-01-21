using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ACME_INC_POE.Models
{
    public class Products
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string Description { get; set; }
        [Display(Name = "R")]
        public decimal Price { get; set; }
        public byte[] Image { get; set; }

    }
}