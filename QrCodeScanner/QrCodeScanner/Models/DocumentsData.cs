using System;
using System.Collections.Generic;
using System.Text;

namespace QrCodeScanner.Models
{
    public class DocumentsData
    {
        public string signMethod { get; set; }
        public List<documentsToSign> documentsToSign { get; set; }

    }
    public class documentsToSign
    {
        public int id { get; set; }
        public string nameRu { get; set; }
        public string nameKz { get; set; }
        public string nameEn { get; set; }  
       
        public List<meta> meta { get; set;}
        public string documentXml { get; set; }

    }
    public class meta
    {
        public string name  { get; set; }
        public string value { get; set; }
    }
 

}
