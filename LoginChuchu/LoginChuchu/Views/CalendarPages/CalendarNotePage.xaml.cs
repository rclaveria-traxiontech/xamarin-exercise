using LoginChuchu.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginChuchu.Views.CalendarPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarNotePage : ContentPage
    {
        public CalendarNotePage(DateTime SelectedDate)
        {
            InitializeComponent();
            BindingContext = new CalendarNoteViewModel(SelectedDate);
        }
    }
}
