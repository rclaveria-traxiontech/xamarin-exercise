using LoginChuchu.Utils;
using LoginChuchu.Views;
using System.ComponentModel;
using System.Net.Http;
using Xamarin.Forms;

namespace LoginChuchu.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient httpClient;
        // Constructor
        public LoginViewModel()
        {
            httpClient = new HttpClient();
            LoginButtonClickCommand = new Command(OnLogin);
            WeatherButtonClickCommand = new Command(OnWeather);
            CalendarButtonClickCommand = new Command(OnCalendar);
        }

        // Triggers when a property is changed in viewmodel
        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        // Private Variables
        private string emailAddress = null;
        private string password = null;
        private Color emailTextColor = Color.Black;

        // Public Variables
        public Command LoginButtonClickCommand { get; }
        public Command WeatherButtonClickCommand { get; }
        public Command CalendarButtonClickCommand { get; }
        private async void OnLogin()
        {
            // Check if Email is Valid and if fields are empty
            if ((RegexUtil.ValidEmailAddress().IsMatch(emailAddress)) &&
                string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(password))
                await Application.Current.MainPage.DisplayAlert("Invalid Credentials", "Wrong Email or Password", "OK");
            
            // If Email Matches, Redirect to HomePage
            if (emailAddress == "frog@email.com" && Password == "frog")
                await Application.Current.MainPage.Navigation.PushAsync(new HomePage(), true);
            // Else display error alert
            else
                await Application.Current.MainPage.DisplayAlert("Invalid Credentials", "Wrong Email or Password", "OK");
        }
        private async void OnWeather()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new WeatherForecastPage(), true);
        }
        private async void OnCalendar()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CalendarPage(), true);
        }

        public string EmailAddress
        {
            get { return emailAddress; }
            set
            {
                // Set Value
                emailAddress = value;
                // Change Styles
                emailTextColor = RegexUtil.ValidEmailAddress().IsMatch(value)
                    ? Color.Green
                    : Color.Black;
                // Trigger Events
                OnPropertyChanged("EmailTextColor");
            }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        // Form Styles
        public Color EmailTextColor
        {
            get { return emailTextColor; }
            set
            {
                emailTextColor = value;
            }
        }

    }
}
