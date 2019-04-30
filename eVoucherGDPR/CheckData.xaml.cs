using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Data.SqlClient;
using Xamarin.Forms.Xaml;
using System.Text;

namespace eVoucherGDPR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckData : ContentPage
    {
        int iznos;
        String alias;
        String broj;

        public CheckData(String al,String iz, String br)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            alias = al;
            broj = br;
            iznos = Int32.Parse(iz);
            tekst.Text = "Odabrani iznos je: " + iznos;
            if(broj=="") PronadiBroj();
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            String iz;
            iz = (String)ValueEntry.Text;

            if (int.TryParse(iz,out int number))
                iznos = Int32.Parse(iz);
            else if(iz!="")
            {
                ValueEntry.Placeholder = "!";
                ValueEntry.PlaceholderColor = Xamarin.Forms.Color.Red;
                ValueEntry.Text = "";
                iznos = 0;
            } else
            {
                ValueEntry.Placeholder = "Unesite iznos...";
                ValueEntry.PlaceholderColor = default;
                iznos = 0;
            }
            tekst.Text = "Odabrani iznos je: " + iznos;
        }

        private async void OnButtonSendClick(object sender, EventArgs e)
        {
            frame_send.ScaleTo(1.2,200);
            button_send.ScaleTo(1.1, 200);
            frame_send.ScaleTo(1,200);
            await button_send.ScaleTo(1, 200);

            if (iznos == 0)
                await DisplayAlert("Napomena", "Potrebno je unijeti valjan iznos.", "OK");
            else if (iznos>200)
                await DisplayAlert("Napomena", "Unijeli ste prevelik iznos.", "OK");
            else
                await Navigation.PushModalAsync(new ResultPage(alias,broj,iznos));  
        }
        private async void On10ButtonClicked(object sender, EventArgs e)
        {
            Button_10.ScaleTo(1.2, 500);
            await Task.Delay(200);
            Button_10.ScaleTo(1, 500);
            iznos = 10;
            tekst.Text = "Odabrani iznos je: " + iznos;
        }
        private async void On20ButtonClicked(object sender, EventArgs e)
        {
            Button_20.ScaleTo(1.2, 500);
            await Task.Delay(200);
            Button_20.ScaleTo(1, 500);
            iznos = 20;
            tekst.Text = "Odabrani iznos je: " + iznos;
        }
        private async void On50ButtonClicked(object sender, EventArgs e)
        {
            Button_50.ScaleTo(1.2, 500);
            await Task.Delay(200);
            Button_50.ScaleTo(1, 500);
            iznos = 50;
            tekst.Text = "Odabrani iznos je: " + iznos;
        }
        private async void On100ButtonClicked(object sender, EventArgs e)
        {
            Button_100.ScaleTo(1.2, 500);
            await Task.Delay(200);
            Button_100.ScaleTo(1, 500);
            iznos = 100;
            tekst.Text = "Odabrani iznos je: " + iznos;
        }

        private void PronadiBroj()
        {
           
                try
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = "evouchergdpr.database.windows.net";
                    builder.UserID = "jpeko00";
                    builder.Password = "Kj73096L";
                    builder.InitialCatalog = "eVoucherDB";

                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {
                        connection.Open();
                        StringBuilder sb = new StringBuilder();
                        sb.Append("SELECT number_ from dbo.ev ");
                        sb.Append("WHERE username_='" + alias + "';");
                        string sql = sb.ToString();
                        SqlCommand cmd = new SqlCommand(sql, connection);
                        SqlDataReader myreader;

                        try
                        {
                            myreader = cmd.ExecuteReader();
                            while (myreader.Read())
                            {
                                broj = myreader.GetString(0);
                            }
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }
                catch (SqlException e)
                { }
            
        }
    }
}