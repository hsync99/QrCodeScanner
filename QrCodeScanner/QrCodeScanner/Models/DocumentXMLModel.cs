using System;
using System.Collections.Generic;
using System.Text;

namespace QrCodeScanner.Models
{
    public class DocumentXMLModel
    {
        public Requisites requisites { get; set; }
        public DocumentsData documentsData { get; set; }
        public DocumentXMLModel() 
        {
            requisites = new Requisites();  
            documentsData = new DocumentsData();    
        }   

    }
}
