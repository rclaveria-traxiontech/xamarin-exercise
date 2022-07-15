using LoginChuchu.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginChuchu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        // Constructor - Binds to ViewModel
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}