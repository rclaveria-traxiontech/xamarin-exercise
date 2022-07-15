using LoginChuchu.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace LoginChuchu.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}