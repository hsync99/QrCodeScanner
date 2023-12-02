using Newtonsoft.Json;
using QrCodeScanner.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QrCodeScanner.ViewModels
{
    [QueryProperty(nameof(ScanResult), nameof(ScanResult))]
    
    public  class DocumentPageViewModel:BaseViewModel
    {
        private string scanResult;
        private string reqjson;
        public string ScanResult { get => scanResult; set => SetProperty(ref scanResult, value); }
        private DocumentXMLModel documentXML;
        public DocumentXMLModel DocumentXML { get => documentXML; set=>SetProperty(ref documentXML, value); }


        private string res;
        public string Res { get => res; set => SetProperty(ref res,value); }
        public DocumentPageViewModel()
        {

            DocumentXML = new DocumentXMLModel();
             
        }
        public async void OnAppearing()
        {
            //  DocumentXML = GetData(DocJson); 
            GetData(ScanResult);
        }
        public async void GetData(string data)
        {
            var req_json = await GetRequisites(data);
            var requisites = JsonConvert.DeserializeObject<Requisites>(req_json);

            var api2_uri = requisites.document.uri;
            var doc_json = await GetDocumentData(api2_uri);
            var doc = JsonConvert.DeserializeObject<DocumentsData>(doc_json);
            DocumentXML = new DocumentXMLModel();
            DocumentXML.requisites = requisites;
            DocumentXML.documentsData = doc;

        }
        public async Task<string> GetRequisites(string scanResult)
        {
            string link = scanResult.Replace("mobileSign:", "");

            string response = await restClient.GetRequisites(link);
            Res = response;
            return response;
        }
        public async Task<string> GetDocumentData(string link)
        {
            return await restClient.GetDocumentData(link);
        }
        public async Task PutDocument(string link, string json)
        {
            await restClient.PutXmlSign(link, json, "");
        }

    }
}
