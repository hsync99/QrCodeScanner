using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QrCodeScanner.Models;
using QrCodeScanner.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QrCodeScanner.ViewModels
{
    [QueryProperty(nameof(ScanResult), nameof(ScanResult))]
    
    public  class DocumentPageViewModel:BaseViewModel
    {
        private string scanResult;
        private string expiry_date;
        private string description;
        private string doc_title;
        private string org_title;
        private string docxml;
        private string sign;
        private string jsonxmlsign;
        private string apiv2URL;
        private string signJSON;
        private bool signEnabled;
        public string ScanResult { get => scanResult; set => SetProperty(ref scanResult, value); }
        private DocumentXMLModel documentXML;
        public DocumentXMLModel DocumentXML { get => documentXML; set=>SetProperty(ref documentXML, value); }

        private ObservableCollection<meta> meta;
        public ObservableCollection<meta> Meta { get => meta; set => SetProperty(ref meta, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }
        public bool SignEnabled { get => signEnabled; set => SetProperty(ref signEnabled, value); }
        public string Expiry_date { get => expiry_date; set => SetProperty(ref expiry_date, value); }
        public string ApiV2URL { get => apiv2URL; set => SetProperty(ref apiv2URL, value); }
        public string SignJSON { get => signJSON; set => SetProperty(ref signJSON, value); }
        public string DocTitle { get => doc_title; set => SetProperty(ref doc_title, value); }
        public string OrgTitle { get => org_title; set => SetProperty(ref org_title, value); }
        public string DocXML { get => docxml; set => SetProperty(ref docxml, value); }
        public string JSONXMLSIGN { get => jsonxmlsign; set => SetProperty(ref jsonxmlsign, value); }

        public string XMLSIGN = "";
        public ICommand SignDocCommand { get; }
        public ICommand ClosePageCommand { get; }
        public DocumentPageViewModel()
        {
           
            DocumentXML = new DocumentXMLModel();
             SignDocCommand  = new Command(async () => await PutDocument());
            ClosePageCommand = new Command(ClosePage);
            XMLSIGN = GetJsonData();
        }
        public async void OnAppearing()
        {
            XMLSIGN = GetJsonData();
            GetData(ScanResult);
            Meta = new ObservableCollection<meta>();


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
            var m =  doc.documentsToSign.First().meta;
            foreach (var meta in m) {
              Meta.Add(meta);
            }
            OrgTitle = requisites.organisation.nameRu;
            Description = OrgTitle + " : " + requisites.description;
            
           DateTime.TryParse(requisites.expiry_date, out DateTime DTE).ToString();
            Expiry_date = "Срок действия до:" + DTE;
            var docstoSign = doc.documentsToSign.FirstOrDefault();
            DocTitle = docstoSign.nameRu;
            DocXML = docstoSign.documentXml;


            string pattern = @"<document>.*?</document>";
            var _docxml = DocXML.Replace("<root><document>", "");
            _docxml = _docxml.Replace("</document></root>", "");
            _docxml = _docxml.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>","");
            // Replace using Regex.Replace method
            string signedxml = Regex.Replace(XMLSIGN, pattern, "<document>" + _docxml + "</document>");
                              

            DocumentsData signedDoc = new DocumentsData();
            signedDoc = doc;
            signedDoc.documentsToSign.First().documentXml = signedxml;
          

            var signeddocJson = JsonConvert.SerializeObject(signedDoc);
            signeddocJson.Replace("documentsData:", "");
            ApiV2URL = api2_uri;
            SignJSON = signeddocJson;
        


        }
        public async Task<string> GetRequisites(string scanResult)
        {
            string link = scanResult.Replace("mobileSign:", "");

            return  await restClient.GetRequisites(link);
        }
        public async Task<string> GetDocumentData(string link)
        {
            SignEnabled = true; 
            return await restClient.GetDocumentData(link);
        }
        public async Task PutDocument()
        {
            var jsonrp = SignJSON;
            var response = await restClient.PutXmlSign(ApiV2URL, jsonrp, "");
            await ShowWarning("Response", response);
        }
        public string GetJsonData()
        {
            string jsonFileName = "MYJSON.json";
            MyJSON ObjContactList = new MyJSON();
            var assembly = typeof(DocumentPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            using (var reader = new System.IO.StreamReader(stream))
            {
                var jsonString = reader.ReadToEnd();

                //Converting JSON Array Objects into generic list    
                ObjContactList = JsonConvert.DeserializeObject<MyJSON>(jsonString);

            }
            //Binding listview with json string     
            return ObjContactList.xml;
        }
        public async  void ClosePage()
        {
            await Shell.Current.Navigation.PopToRootAsync();

        }

    }
}
