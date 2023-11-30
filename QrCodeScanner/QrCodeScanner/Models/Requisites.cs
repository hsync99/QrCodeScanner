using System;
using System.Collections.Generic;
using System.Text;

namespace QrCodeScanner.Models
{
    public class Requisites
    {
        public string description { get; set; }
        public string expiry_date { get; set; }
        public document document { get; set; }
        public organization organisation { get; set; }  
    }
}
