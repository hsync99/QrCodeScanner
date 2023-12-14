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
       
        public DocumentsData DocumentsData { get; set; }
        public DocumentXMLModel DocumentXML { get; set; }

        private bool isactive = false;
        public bool IsActive { get => isactive; set => SetProperty(ref isactive, value); }
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));

            MessagingCenter.Subscribe<App, string>(this, "ScanData", async (sender, data) =>
            {
               
                RouteToDocuemntPage(data);
            });
        }

        public ICommand OpenWebCommand { get; }
        public ICommand GoToDocumentPageCommand { get; }

      
        public async void RouteToDocuemntPage(string data)
        {

            await Shell.Current.GoToAsync($"{nameof(DocumentPage)}?{nameof(DocumentPageViewModel.ScanResult)}={data}");
        }
    }
}