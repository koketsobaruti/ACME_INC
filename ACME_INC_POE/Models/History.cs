using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACME_INC_POE.Models
{
    public class History
    {
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
    }
}