using Newtonsoft.Json;
using QrCodeScanner.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QrCodeScanner.ViewModels
{
    [QueryProperty(nameof(DocJson), nameof(DocJson))]
    [QueryProperty(nameof(ReqJson), nameof(ReqJson))]
    public  class DocumentPageViewModel:BaseViewModel
    {
        private string docjson;
        private string reqjson;
        public string DocJson { get => docjson; set => SetProperty(ref docjson, value); }
        public string ReqJson { get => reqjson; set => SetProperty(ref reqjson, value); }
        private DocumentXMLModel documentXML;
        public DocumentXMLModel DocumentXML { get => documentXML; set=>SetProperty(ref documentXML, value); }   
        
        public DocumentPageViewModel()
        {

            DocumentXML = new DocumentXMLModel();
             
        }
        public async void OnAppearing()
        {
            DocumentXML = GetData(DocJson, ReqJson); 
        }
        public DocumentXMLModel GetData(string doc,string req)
        {
            var documentsData = JsonConvert.DeserializeObject<DocumentsData>(doc);
            var requisites = JsonConvert.DeserializeObject<Requisites>(req);
            DocumentXML = new DocumentXMLModel();

            DocumentXML.documentsData = documentsData;
            DocumentXML.requisites = requisites;
            return DocumentXML;

        }

    }
}
