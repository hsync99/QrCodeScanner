using QrCodeScanner.ViewModels;
using QrCodeScanner.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace QrCodeScanner
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(DocumentPage), typeof(DocumentPage));
        }

    }
}
