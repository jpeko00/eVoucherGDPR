using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using eVoucherGDPR.Views;
using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace eVoucherGDPR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        String alias;
        String number;
        int amount;
        String info_text;
        String info_code;
        int info_img = 0;
        int tran_id = 0;
        int error_code;
        public ResultPage(String al, String num, int am)
        {
            
            NavigationPage.SetHasNavigationBar(this, false);
            alias = al;
            number = num;
            amount = am;

            StartConnectionAsync();
            InitializeComponent();
            ScaleImage();

        }
        private async Task StartConnectionAsync()
        {
            String api = "https://evouchergdpr.azurewebsites.net/api/Values?alias=" + alias + "&number=" + number + "&amount=" + amount;

            HttpWebResponse response = null;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(api);
            request.Method = "GET";
            String str="";

            try { 
                using (response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    StreamReader sr = new StreamReader(response.GetResponseStream());
                    str = sr.ReadToEnd();
                    tran_id = Int32.Parse(str.Substring(1, str.IndexOf(",") - 1));
                    error_code = Int32.Parse(str.Substring(str.Length - 2, 1));
                }
            }
            catch
            {
                error_code = 5;
            }
                       
            SetValues();
        }

        private void SetValues()
        {
            switch (error_code)
            {
                case 1:
                    info_text = "Dogodila se pogreška prilikom unošenja podataka u bazu.";
                    info_code = "Kôd pogreške: 1 \n";
                    break;
                case 2:
                    info_text = "Dogodila se pogreška prilikom povezivanja na server.";
                    info_code = "Kôd pogreške: 2 \n";
                    break;
                case 3:
                    info_text = "Transakcija je uspješno izvršena.";
                    info_code = "Transakcija:" + tran_id + "\n";
                    info_img = 1;
                    break;
                case 4:
                    info_text = "Dogodila se pogreška u izvršavanju transakcije.";
                    info_code = "Kôd pogreške: 3 \n";
                    break;
                case 5:
                    info_text = "Molimo provjerite jeste li povezani na Internet.";
                    info_code = "Kôd pogreške: 5\n";
                    break;
                default:
                    info_text = "Nepoznata pogreška.";
                    info_code = "Kôd pogreške: 4 \n";
                    break;
            }

            label_info_code.Text = info_code;
            label_info_text.Text = info_text;

            if (info_img == 1)
                img_info.Source = "cropped_check.png";
            else
                img_info.Source = "cropped_x.png";
        }

        private async void ScaleImage()
        {
            await img_info.ScaleTo(1.5, 500);
            await img_info.RotateTo(360, 800);
            await img_info.ScaleTo(1, 500);

            img_info.Rotation = 0;
        }

    }
}