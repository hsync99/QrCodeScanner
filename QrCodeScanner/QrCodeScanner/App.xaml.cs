using QrCodeScanner.Services;
using QrCodeScanner.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QrCodeScanner
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<RestClientService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
