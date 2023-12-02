using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Newtonsoft.Json;
using QrCodeScanner.Models;
using QrCodeScanner.Views;
using ZXing;

namespace QrCodeScanner.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string MItem { get; set; }
        private string res;
        public DocumentsData DocumentsData { get; set; }
        public DocumentXMLModel DocumentXML { get; set; }
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

                RouteToDocuemntPage(doc_json, req_json);
            });
        }

        public ICommand OpenWebCommand { get; }
        public ICommand GoToDocumentPageCommand { get; }

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
        public async void RouteToDocuemntPage(string doc, string req)
        {

            //await Task.FromResult(Xamarin.Forms.Shell.Current.GoToAsync($"{nameof(OrderDetailView)}?{nameof(OrderDetailViewModel.ItemId)}={item.uid}"));
            //await Task.FromResult(Xamarin.Forms.Shell.Current.GoToAsync($"{nameof(DocumentPage)}"));
            // await Shell.Current.GoToAsync($"{nameof(DocumentPage)}?{nameof(DocumentPageViewModel.DocJson)}={doc}");
           // await Shell.Current.GoToAsync($"{nameof(JobTransactionDetailPage)}?{nameof(JobTransactionDetailViewModel.TransactionId)}={obj.Id}&
           // {nameof(JobTransactionDetailViewModel.OperationCode)}={obj.OperationCode}");
            await Shell.Current.GoToAsync($"{nameof(DocumentPage)}?{nameof(DocumentPageViewModel.DocJson)}={doc}&{nameof(DocumentPageViewModel.ReqJson)}={req}");
        }
    }
}