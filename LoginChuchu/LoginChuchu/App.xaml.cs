using LoginChuchu.Services;
using LoginChuchu.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginChuchu
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            // Start on Login Page
            MainPage = new NavigationPage(new LoginPage()) {
                BarBackgroundColor = Color.FromHex("61402d")
            };
            // MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
