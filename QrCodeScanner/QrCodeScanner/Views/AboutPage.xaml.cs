using QrCodeScanner.ViewModels;
using System;
using System.ComponentModel;
using System.Net.NetworkInformation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
namespace QrCodeScanner.Views
{
    public partial class AboutPage : ContentPage
    {
        AboutViewModel vm;
        public AboutPage()
        {
            InitializeComponent();
            vm = (AboutViewModel)BindingContext;

        }

        private async void qrview_OnScanResult(ZXing.Result result1)
        {
            qrview.OnScanResult += (result) =>
          Device.BeginInvokeOnMainThread(async () =>
            {
     
                qrview.IsScanning = false;
                MessagingCenter.Send<App, string>((App)Xamarin.Forms.Application.Current, "ScanData", result.Text);
            });
            //string link = result1.Text.Replace("mobileSign:", "");

            //var data = await vm.GetRequisites(link);
            //ScanResult.Text = data;
        }
    }
}