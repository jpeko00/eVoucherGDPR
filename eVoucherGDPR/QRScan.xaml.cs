using System;
using Xamarin.Forms;
using System.Windows;
using Xamarin.Forms.Xaml;
using ZXing;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace eVoucherGDPR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRScan : ContentPage
    {

        String res="";
        String alias = "";
        String iznos = "0";
        String broj = "";//neka on postavi ovako alias i iznos
        public QRScan()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            this.SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();
            zxing.IsScanning = true;
            zxing.IsAnalyzing = true;
            
            zxing.OnScanResult += (result) =>
            {
                OnScanResult(result);
            };
        }
       public void OnScanResult(ZXing.Result result)
        {
            res = (String)result.Text;
            zxing.IsScanning = false;
            next.IsEnabled = true;
            alias = res.Substring(res.IndexOf(".") + 1,res.IndexOf(",")-res.IndexOf(".")-1); // 50.marija20,098986 index 4 (m) i onda 11-2 -1
            iznos = res.Substring(0, res.IndexOf(".")); //5 i dva znaka
            broj = res.Substring(res.IndexOf(",") + 1);//od indexa 9. do kraja
           
        }

            private async void OnNextButtonClicked(object sender, EventArgs e)
            {
            
            if (alias != "")
            {
                entry_alias.IsEnabled = false;
                try
                {
                    if (!string.IsNullOrWhiteSpace(entry_alias.Text))
                    {
                        String api = "https://evouchergdpr.azurewebsites.net/api/Values?alias=" + alias;

                        HttpWebResponse response = null;
                        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(api);
                        request.Method = "GET";
                        String str;

                        using (response = (HttpWebResponse)await request.GetResponseAsync())
                        {
                            StreamReader sr = new StreamReader(response.GetResponseStream());
                            str = sr.ReadToEnd();
                        }

                        int number = int.Parse(str.Substring(1,str.Length-2));

                        if (number>0)
                        {
                            await Navigation.PushModalAsync(new NavigationPage(new CheckData(alias, iznos, broj)));
                        }
                        else
                        {
                            await DisplayAlert("Napomena", "Uneseni alias ne postoji.", "OK");
                            entry_alias.IsEnabled = true;
                        }
                    }

                    else
                    await Navigation.PushModalAsync(new NavigationPage(new CheckData(alias, iznos, broj)));
                }
                catch (Exception ex)
                {
                    String aaa = ex.ToString();
                    int a = 2;
                }
            }
            else 
                await DisplayAlert("Napomena", "Potrebno je skenirati kôd ili unijeti alias.", "OK");
        }

        private void OnResetButtonClicked(object sender, EventArgs e)
        {
           zxing.Result = null;
           zxing.IsScanning = true;
           zxing.IsAnalyzing = true;
        }
       
        private void Overlay_OnFlashButtonClicked(Button sender, EventArgs e)
        {
            zxing.IsTorchOn = !zxing.IsTorchOn;
        }
        public void OnEntryTextChanged(object sender, EventArgs e)
        {
            alias = entry_alias.Text.ToString();
        }


    }
}