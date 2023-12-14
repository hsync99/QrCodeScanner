using QrCodeScanner.Services;
using QrCodeScanner.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Mobile;
using static ZXing.Mobile.MobileBarcodeScanningOptions;

namespace QrCodeScanner.Views
{
    public partial class AboutPage : ContentPage
    {
        AboutViewModel vm;
        public AboutPage()
        {
            InitializeComponent();
            vm = (AboutViewModel)BindingContext;
            var options = new ZXing.Mobile.MobileBarcodeScanningOptions()
            {
                PossibleFormats = new List<ZXing.BarcodeFormat>() { ZXing.BarcodeFormat.QR_CODE },
               UseNativeScanning = true
            };

            qrview.Options = options;   
        }

        private async void qrview_OnScanResult(ZXing.Result result1)
        {
            qrview.OnScanResult += (result) =>
          Device.BeginInvokeOnMainThread(async () =>
            {
                string res = result.Text;
                if (!String.IsNullOrEmpty(replacefrom.Text) && !String.IsNullOrWhiteSpace(replacefrom.Text)){
                    res = res.Replace(replacefrom.Text, replaceto.Text);
                }
                scanBtn.IsVisible = true;   
                MessagingCenter.Send<App, string>((App)Xamarin.Forms.Application.Current, "ScanData", res);
            });
            //string link = result1.Text.Replace("mobileSign:", "");

            //var data = await vm.GetRequisites(link);
            //ScanResult.Text = data;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            qrview.IsScanning = true;
           
        }
        protected override void OnDisappearing()
        {
            qrview.IsScanning = false;
            base.OnDisappearing();

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
             
            qrview.IsScanning = true;
            scanBtn.IsVisible = false;
            OnAppearing();
            try
            {
                var existingPages = Navigation.NavigationStack.ToList();
                foreach (var page in existingPages)
                {
                    Navigation.RemovePage(page);
                }
            }
            catch (Exception ex)
            {

            }
            
            Shell.Current.CurrentItem = new AboutPage();
        }
    }
}