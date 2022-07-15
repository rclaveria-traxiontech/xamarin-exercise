using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LoginChuchu.ViewModels
{
    public class CalendarNoteViewModel : INotifyPropertyChanged
    {
        public DateTime Date { get; }
        public CalendarNoteViewModel(DateTime SelectedDate)
        {
            Date = SelectedDate;
            OnPropertyChanged("Date");
        }
        // Sets PropertyChanged Event
        public event PropertyChangedEventHandler PropertyChanged;
        // Triggers when a property is changed in viewmodel
        #region INotifyPropertyChanged implementation
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
