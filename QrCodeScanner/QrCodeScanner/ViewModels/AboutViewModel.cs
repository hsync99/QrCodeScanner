using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Newtonsoft.Json;
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
              var r = await GetRequisites(data);
                

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
        public async void GetDocumentData(string link)
        {
            var docdata = await restClient.GetDocumentData(link);   
        }
    }
}