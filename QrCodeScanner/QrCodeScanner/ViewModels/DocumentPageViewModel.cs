using Newtonsoft.Json;
using QrCodeScanner.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
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
        public string ScanResult { get => scanResult; set => SetProperty(ref scanResult, value); }
        private DocumentXMLModel documentXML;
        public DocumentXMLModel DocumentXML { get => documentXML; set=>SetProperty(ref documentXML, value); }

        public string Description { get => description; set => SetProperty(ref description, value); }
        
        public string Expiry_date { get => expiry_date; set => SetProperty(ref expiry_date, value); }
        public string DocTitle { get => doc_title; set => SetProperty(ref doc_title, value); }
        public string OrgTitle { get => org_title; set => SetProperty(ref org_title, value); }
        public string DocXML { get => docxml; set => SetProperty(ref docxml, value); }
        public string XMLSIGN = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?><root><document>2b3780359f6e98120053b6a547cc911ef30bea8ee6fe309cb3d0aab0cb538e16</document><ds:Signature xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\">\r\n<ds:SignedInfo>\r\n<ds:CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\"/>\r\n<ds:SignatureMethod Algorithm=\"http://www.w3.org/2001/04/xmldsig-more#rsa-sha256\"/>\r\n<ds:Reference URI=\"\">\r\n<ds:Transforms>\r\n<ds:Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\"/>\r\n<ds:Transform Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments\"/>\r\n</ds:Transforms>\r\n<ds:DigestMethod Algorithm=\"http://www.w3.org/2001/04/xmlenc#sha256\"/>\r\n<ds:DigestValue>zjfWQ67gA4UPgwZpQ1ANtzTr590T99KTPEK87Xtmnlk=</ds:DigestValue>\r\n</ds:Reference>\r\n</ds:SignedInfo>\r\n<ds:SignatureValue>\r\nLPuVRe+OB5veMhHq/MuKMm6A9vkJIZtS8p9SkOuOwzqdk9REmx5dUvHyMBi5yygz0A8NxjVgKcL3\r\neQ0+CmAyKup3mf3huKCyqpggS/Vw7sr7sFpym77ObFKLWavqTKwqRElgI2lwQbA+6z8KM35o/ZW+\r\nzPV9OnWIVuCCc3H+ipbCVATRTB+VM4zANCrh5swESW1S8t+7MVeweysPc8TTnPvbm6QFtq9ySpm2\r\niOYVRZnfipFQ7BnNTJpnlsx9SHXQKxGdc4EC3+RzkB/bcMrNJFTdc/CFmbNIFfCwTATUdlLTCOq6\r\n2ahJrzrqX2czpdjc2SUuyC+UVKsaUnVeIU+Zig==\r\n</ds:SignatureValue>\r\n<ds:KeyInfo>\r\n<ds:X509Data>\r\n<ds:X509Certificate>\r\nMIIGEzCCA/ugAwIBAgIUZB5ylSGYai0S5Rh9xsYFeJ4jZe8wDQYJKoZIhvcNAQELBQAwLTEeMBwG\r\nA1UEAwwV0rDSmtCeIDMuMCAoUlNBIFRFU1QpMQswCQYDVQQGEwJLWjAeFw0yMzExMTYwMzI1NTFa\r\nFw0yNDExMTUwMzI1NTFaMHkxHjAcBgNVBAMMFdCi0JXQodCi0J7QkiDQotCV0KHQojEVMBMGA1UE\r\nBAwM0KLQldCh0KLQntCSMRgwFgYDVQQFEw9JSU4xMjM0NTY3ODkwMTExCzAJBgNVBAYTAktaMRkw\r\nFwYDVQQqDBDQotCV0KHQotCe0JLQmNCnMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA\r\ni3X1U2840U28xKltl8b5McKfiCRkUmjmksyDXv689Htoc6Z09QjznTsiJnBAR2LxVZQwAsV38sfz\r\nYE4PPIhZ8buaHeZ7FTUlRqi8EwIPOIIGMCba19uHHkMOSQ7XP2jbJ515a96POa43BYjw90hwuqyt\r\nvekGY+7DhzpY26HS4e3usOfyimxcI54rxMKmXtM/ctbdWS02mBeraTnQCE7J7H0ONRX2OHtBBOq3\r\n7YCV3mXx+2tXcLUmm/pBZ3HtApSK0NYw4nEsSM2GfAuhMBEyaEOwFLcOcQ4MgbeMlaKOE1hCsjjd\r\nj/VVNSrmv1yV0CQB7AImavkuGjsYywRNAllCTwIDAQABo4IB3TCCAdkwDgYDVR0PAQH/BAQDAgbA\r\nMB0GA1UdJQQWMBQGCCsGAQUFBwMEBggqgw4DAwQBATBeBgNVHSAEVzBVMFMGByqDDgMDAgMwSDAh\r\nBggrBgEFBQcCARYVaHR0cDovL3BraS5nb3Yua3ovY3BzMCMGCCsGAQUFBwICMBcMFWh0dHA6Ly9w\r\na2kuZ292Lmt6L2NwczA8BgNVHR8ENTAzMDGgL6AthitodHRwOi8vdGVzdC5wa2kuZ292Lmt6L2Ny\r\nbC9uY2FfcnNhX3Rlc3QuY3JsMD4GA1UdLgQ3MDUwM6AxoC+GLWh0dHA6Ly90ZXN0LnBraS5nb3Yu\r\na3ovY3JsL25jYV9kX3JzYV90ZXN0LmNybDByBggrBgEFBQcBAQRmMGQwOAYIKwYBBQUHMAKGLGh0\r\ndHA6Ly90ZXN0LnBraS5nb3Yua3ovY2VydC9uY2FfcnNhX3Rlc3QuY2VyMCgGCCsGAQUFBzABhhxo\r\ndHRwOi8vdGVzdC5wa2kuZ292Lmt6L29jc3AvMB0GA1UdDgQWBBRkHnKVIZhqLRLlGH3GxgV4niNl\r\n7zAfBgNVHSMEGDAWgBQLx+3PaJY6SUfFv/H357jtwu04IDAWBgYqgw4DAwUEDDAKBggqgw4DAwUB\r\nATANBgkqhkiG9w0BAQsFAAOCAgEAjG6Kd838BOQyFjKuR3WMpQlTm60wxUtlIpx02UFbr+HSXy1C\r\ndicWYCbbgLfVGaYvYoAWa2tnwjStM+YTBxPEl3BbyNHSqwvh0rmO136ZpOpX7FgapWJwU7iilUWL\r\n0j7PdlA7B+SxWiQQ3MqFLAsUtblPtZIIod9Yz89bew4EAHBBUr1rPKmmUUrZWc4EyG7j17bcVwAC\r\nAMb9dkqW5hFWbjRNuoLwx9BfHHtUJL5jzuAoCUI1Bm2Ba5Q/tvC/tp85pxqUjdDT23RHPre/Oc4Y\r\n+e1c2la0vCQufxXPdgDEf8kk1pc/33/jtC6bGK1jHSiRzSo0Dq0HKuP81nbubZWnMpydNe/R2Cbc\r\na4MDyFCrWZrQjHr9tujD+vxxwiW/4zBUauuga8UlMvFIEFe3Dp0PHxiyEGp/sBuKjOIxHwDhaP+M\r\nnkkYszTDhdVjfUb00gVK7B9A9H3Y/uFbJ6Yz+udgf46zqY+JibW0cfEj5EaZPvsOMT/wSAYz7b32\r\n9aK0fEtZQORuUx0C6IGr2brUOGYN6kaIcQENvEOl3EsS/yDm32YVLq8Qj7AESIfcpYE3c1TmPXCG\r\nLOKWQxv8ORv+vUA3Pl4lCS9b7RxWWV6o+oBVdtloxq988RyS8uCJfIr1rXjpU0o5d9X84dOfmMHW\r\n4VnobwdHfDjpPDiGX9K4okx0k6A=\r\n</ds:X509Certificate>\r\n</ds:X509Data>\r\n</ds:KeyInfo>\r\n</ds:Signature></root>";
        public string Sign { get => sign; set => SetProperty(ref sign, value); }
        public DocumentPageViewModel()
        {

            DocumentXML = new DocumentXMLModel();
             
        }
        public async void OnAppearing()
        {
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

            OrgTitle = requisites.organisation.nameRu;
            Description = OrgTitle + " : " + requisites.description;
            
           DateTime.TryParse(requisites.expiry_date, out DateTime DTE).ToString();
            Expiry_date = "Срок действия до:" + DTE;
            var docstoSign = doc.documentsToSign.FirstOrDefault();
            DocTitle = docstoSign.nameRu;
            DocXML = docstoSign.documentXml;


            string xmlString = DocXML.Replace('{',' ');

            xmlString = xmlString.Replace('}', ' ');
            // Parse the XML string into an XDocument
        
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            // Find the <document> node and update its value
            XmlNodeList documentHash = xmlDoc.GetElementsByTagName("document");

            XmlDocument xmlSign = new XmlDocument();
            xmlSign.LoadXml(XMLSIGN);
            XmlNodeList signHash = xmlSign.GetElementsByTagName("document");

            signHash[0].InnerText = documentHash[0].InnerText;
            DocumentsData signedDoc = new DocumentsData();
            signedDoc = doc;
            signedDoc.documentsToSign.First().documentXml = xmlSign.OuterXml;
            Sign = xmlSign.OuterXml;

            var signeddocJson = JsonConvert.SerializeObject(signedDoc);
            signeddocJson.Replace("documentsData:", "");
            signeddocJson.Replace('\r', ' ');
            var signResponse = await PutDocument(api2_uri, signeddocJson);


        }
        public async Task<string> GetRequisites(string scanResult)
        {
            string link = scanResult.Replace("mobileSign:", "");

            return  await restClient.GetRequisites(link);
        }
        public async Task<string> GetDocumentData(string link)
        {
            return await restClient.GetDocumentData(link);
        }
        public async Task<string> PutDocument(string link, string json)
        {
            var response = await restClient.PutXmlSign(link, json, "");
            return response;
        }

    }
}
