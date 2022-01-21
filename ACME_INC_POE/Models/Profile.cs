using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACME_INC_POE.Models
{
    public class Profile
    {
        public string Username { get; set; }
        public string ApartmentName { get; set; }
        public string Address { get; set; }
        public int CardNumber { get; set; }
        public int CVV { get; set; }
        public bool Default { get; set; }
    }
}