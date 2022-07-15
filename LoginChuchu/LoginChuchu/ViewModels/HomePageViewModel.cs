using LoginChuchu.Views;
using Xamarin.Forms;

namespace LoginChuchu.ViewModels
{
    public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            LogoutButtonClickCommand = new Command(OnLogout);
        }

        // Public Variables
        public Command LogoutButtonClickCommand { get; }
        private async void OnLogout()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage(), true);
        }
    }
}
