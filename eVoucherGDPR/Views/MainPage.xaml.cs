using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace eVoucherGDPR.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.SetValue(NavigationPage.HasNavigationBarProperty, false);
        }

        private async void onButtonReadManualReleased(object sender, EventArgs e)
        {
            try
            {
                await Task.Delay(210);
                await Navigation.PushAsync(new ReadManual());   
            }
            catch (Exception ex)
            { 
            }
        }
        private async void onButtonReadManualPressed(object sender, EventArgs e)
        {
            button_read_manual.ScaleTo(1.2, 200);
            await frame_manual.ScaleTo(1.2, 200);
            button_read_manual.ScaleTo(1, 200);
            await frame_manual.ScaleTo(1, 200);
        }
        private async void OnButtonScanQRReleased(object sender, EventArgs e)
        {
            try
            {
                await Task.Delay(210);
                await Navigation.PushModalAsync(new NavigationPage(new QRScan()));
            }
            catch (Exception ex)
            {

            }
        }

        private async void OnButtonScanQRPressed(object sender, EventArgs e)
        {

            button_read_code.ScaleTo(1.2, 200);
            await frame_qr.ScaleTo(1.2,200);
            button_read_code.ScaleTo(1, 200);
            await frame_qr.ScaleTo(1,200);
           
            //frame_qr.BackgroundColor = Color.PaleVioletRed;
        }
       
    }
}
