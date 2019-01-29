using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace Kantsevich_qrreader
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var scanPage = new ZXingScannerPage();
            await Navigation.PushAsync(scanPage);
            scanPage.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
               {
                   await Navigation.PopAsync();
                   CodeResult.Text = result.Text;
                   if (Uri.IsWellFormedUriString(result.Text, UriKind.Absolute))
                   {                     
                       CodeResult.GestureRecognizers.Add(
                        new TapGestureRecognizer()
                        {
                            Command = new Command(async () =>
                            {
                                await Task.Run(() => { Device.OpenUri(new Uri(result.Text)); });
                            })
                        }
                    );
                   }                     
               });
            };
        }
    }
}
