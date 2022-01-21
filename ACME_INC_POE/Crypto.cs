﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ACME_INC_POE
{
    public class Crypto
    {
        public static string Hash(string val)
        {
            //convert to byte and then hash
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(val))
                );
        }
    }
}