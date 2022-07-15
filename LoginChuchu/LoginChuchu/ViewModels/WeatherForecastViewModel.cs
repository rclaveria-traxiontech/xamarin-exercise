using LoginChuchu.Collections;
using LoginChuchu.Models;
using LoginChuchu.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using Xamarin.Forms;

namespace LoginChuchu.ViewModels
{
    public class WeatherForecastViewModel : INotifyPropertyChanged
    {
        #region fields
        // Fields for OpenWeatherAPI call
        private const string URL = "https://api.openweathermap.org/data/2.5/weather?";
        private const string APPID = "a49ea5246e71b5c07979e6841b393008";
        private const string UNITS = "metric";
        private const string LANG = "en";
        // Fields for Binding City in picker
        private string city = "Taguig";
        private CityModel selectedCity;
        // Fields for changing Background Image
        private short weatherID;
        private string backgroundImage = "atmosphere_bg";
        // Field for HttpClient that sends api requests
        private HttpClient httpClient;
        
        #endregion

        #region properties
        // Collection for Weather Forecast
        public ObservableCollection<OneDayForecastClass> OneDayForecast { get; set; }
        // Collection for Cities
        public ObservableCollection<CityModel> Cities { get; }
        // Sets HttpClient
        public HttpClient HttpClient => httpClient ?? (httpClient = new HttpClient());
        // Sets PropertyChanged Event
        public event PropertyChangedEventHandler PropertyChanged;
        // Serves
        public CityModel SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                City = selectedCity.CityName;
                OnPropertyChanged("City");
            }
        }
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                GetOneDayForecast();
            }
        }
        public short WeatherID
        {
            get { return weatherID; }
            set
            {
                weatherID = value;
                if (weatherID > 800)
                    backgroundImage = "clouds_bg";
                else if (weatherID == 800)
                    backgroundImage = "clear_bg";
                else if (weatherID >= 700)
                    backgroundImage = "atmosphere_bg";
                else if (weatherID >= 600)
                    backgroundImage = "snow_bg";
                else if (weatherID >= 500)
                    backgroundImage = "rain_bg";
                else if (weatherID >= 300)
                    backgroundImage = "drizzle_bg";
                else if (weatherID >= 200)
                    backgroundImage = "thunderstorm_bg";

                OnPropertyChanged("BackgroundImage");
            }
        }
        public string BackgroundImage
        {
            get { return backgroundImage; }
            set { backgroundImage = value; }
        }
        #endregion

        #region methods
            #region constructor
            public WeatherForecastViewModel()
            {
                OneDayForecast = new ObservableCollection<OneDayForecastClass>();
                Cities = new ObservableCollection<CityModel>();
                httpClient = new HttpClient();
                PopulateCityModel();
                GetOneDayForecast();
            }
            #endregion
        private async void GetOneDayForecast()
        {
            // Try catch for null response, no internet
            try
            {
                string url = $"{URL}q={city}&appid={APPID}&units={UNITS}&lang={LANG}";
                HttpResponseMessage jsonResponse = await httpClient.GetAsync(url);
                if (jsonResponse == null)
                    await Application.Current.MainPage.DisplayAlert(
                    "No Internet",
                    "No Internet Connection. Make sure that Wi-Fi or Mobile Data is turned on, then try again",
                    "OK");
                if (!jsonResponse.IsSuccessStatusCode)
                {
                    // Error 401
                    if (jsonResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Application.Current.MainPage.DisplayAlert("Unauthorized", "Access is Denied.", "OK");
                    }
                }
                else
                {
                    string jsonForecast = await jsonResponse.Content.ReadAsStringAsync();
                    var weather = OneDayForecastClass.FromJson(jsonForecast);
                    OneDayForecast.Clear();
                    OneDayForecast.Add(weather);
                    WeatherID = OneDayForecast[0].Weather[0].Id;
                    OnPropertyChanged("WeatherID");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Application.Current.MainPage.DisplayAlert(
                    "Something went wrong",
                    ex.Message,
                    "OK");
            }

        }
        private void PopulateCityModel()
        {
            Cities.Clear();
            Cities.Add(CityModel.FromJson("{\"ID\":1,\"CityName\":\"Pasig\"}"));
            Cities.Add(CityModel.FromJson("{\"ID\":2,\"CityName\":\"Makati\"}"));
            Cities.Add(CityModel.FromJson("{\"ID\":3,\"CityName\":\"Manila\"}"));
            Cities.Add(CityModel.FromJson("{\"ID\":4,\"CityName\":\"Taguig\"}"));
            Cities.Add(CityModel.FromJson("{\"ID\":4,\"CityName\":\"Apayao\"}"));
            Cities.Add(CityModel.FromJson("{\"ID\":4,\"CityName\":\"Davao\"}"));
            Cities.Add(CityModel.FromJson("{\"ID\":4,\"CityName\":\"Cebu\"}"));
            Cities.Add(CityModel.FromJson("{\"ID\":4,\"CityName\":\"Iloilo\"}"));
            Cities.Add(CityModel.FromJson("{\"ID\":4,\"CityName\":\"Japan\"}"));

        }
        
        // Triggers when a property is changed in viewmodel
            #region INotifyPropertyChanged implementation
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        #endregion

    }
}
