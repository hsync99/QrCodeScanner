using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Newtonsoft.Json;
using QrCodeScanner.Models;

namespace QrCodeScanner.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string MItem { get; set; }
        private string res;
        public string Res { get => res; set => SetProperty(ref res, value); }
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            MessagingCenter.Subscribe<App, string>(this, "ScanData", async (sender, data) =>
            {
                var req_json = await GetRequisites(data);
                var requisites = JsonConvert.DeserializeObject<Requisites>(req_json); 

                var api2_uri = requisites.document.uri;
                var doc_json = await GetDocumentData(api2_uri);
                await PutDocument(api2_uri, doc_json);

            });
        }

        public ICommand OpenWebCommand { get; }

        public async Task<string> GetRequisites(string scanResult)
        {
            string link = scanResult.Replace("mobileSign:", "");
           
            string response = await restClient.GetRequisites(link);
            Res = response;
            return response;
        }
        public async Task<string> GetDocumentData(string link)
        {
            return  await restClient.GetDocumentData(link);   
        }
        public async Task PutDocument(string link, string json)
        {
            await restClient.PutXmlSign(link,json,"");
        }
    }
}