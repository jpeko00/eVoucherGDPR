using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using eVoucherGDPR.Views;

namespace eVoucherGDPR
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());
            MainPage.SetValue(NavigationPage.HasNavigationBarProperty, false);
            //MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#173d7c")); //163c7c


        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
