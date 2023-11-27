using QrCodeScanner.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        private void qrview_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                ScanResult.Text = result.Text;
                string link = result.Text.Replace("mobileSign:","");
                var data = await vm.GetRequisites(link);       
                ScanResult.Text = data;
                

            });
        }
    }
}