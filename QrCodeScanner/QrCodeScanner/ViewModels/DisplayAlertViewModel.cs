using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QrCodeScanner.ViewModels
{
    [QueryProperty(nameof(Title), nameof(Title))]
    [QueryProperty(nameof(Message), nameof(Message))]
    public class DisplayAlertViewModel:BaseViewModel
    {
        private string title;

        private string message;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }
        internal async void OnAppearing()
        {
            //await CloseMassage();
            await Task.CompletedTask;
        }
        async Task CloseMassage()
        {
            await Task.Delay(3000);
            try
            {
                await App.Current.MainPage.Navigation.PopPopupAsync();
            }
            catch (Exception e)
            {

            }

        }
    }
}
