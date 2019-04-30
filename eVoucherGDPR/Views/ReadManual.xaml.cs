using eVoucherGDPR.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eVoucherGDPR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReadManual : ContentPage
    {
        // tap.GestureRecognizers.Add(taprec);

        public ReadManual()
        {
            InitializeComponent();
            Navigation.PopAsync();
            BindingContext = new ViewModel.ManualItemsViewModel();
        }

        public void IsTapped(object sender, EventArgs e)
        {
            var imgcell = (ImageCell)sender;
            var item = (ManualItem)imgcell.BindingContext;
            DisplayAlert(item.keyWords,item.description,"OK");
            //DisplayAlert("{Binding keyWords}", "Potrebno je skenirati kôd.", "OK");
        }

        //  taprec.Tapped += (s, e) => {
        // handle the tap
        //};

    }
}