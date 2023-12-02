using QrCodeScanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QrCodeScanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentPage : ContentPage
    {
        DocumentPageViewModel vm;
        public DocumentPage()
        {
            InitializeComponent();
            vm = (DocumentPageViewModel)BindingContext;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.OnAppearing();
        }
    }
}