
using LoginChuchu.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace LoginChuchu.ViewModels
{
    public class CalendarViewModel : INotifyPropertyChanged
    {
        public DateTime SelectedDate { get; set; }
        public List<Month> MonthList { get; set; }
        public List<Year> YearList { get; set; }
        public Month SelectedMonth {
            get => selectedMonth;
            set {
                selectedMonth = value;
                GenerateCalendar(DateTime.Parse(selectedMonth.MonthName + " 1, " + selectedYear.YearNumber));
                OnPropertyChanged("Calendar");
            }
        }
        // 
        private Month selectedMonth = new Month() { MonthName = DateTime.Now.ToString("MMMM"), };
        public Year SelectedYear {
            get => selectedYear;
            set {
                selectedYear = value;
                GenerateCalendar(DateTime.Parse(selectedMonth.MonthName + " 1, " + selectedYear.YearNumber));
                OnPropertyChanged("Calendar");
            }

        }
        private Year selectedYear = new Year() { YearNumber = DateTime.Now.Year.ToString(), };
        // Sets PropertyChanged Event
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<CalendarModel> Calendar { get; set; }
        public Command<DateTime> CalendarDay_ClickedCommand { get; }
        private async void OnCalendarDay_Clicked(DateTime DateClicked)
        {
            if (DateClicked != null)
            {
                SelectedDate = DateClicked;
                OnPropertyChanged("SelectedDate");
                await Application.Current.MainPage.Navigation.PushAsync(
                    new Views.CalendarPages.CalendarNotePage(SelectedDate), true);
            }
            
        }

        #region Functions
        
        public CalendarViewModel()
        {
            Calendar = new ObservableCollection<CalendarModel>();
            CalendarDay_ClickedCommand = new Command<DateTime>(OnCalendarDay_Clicked);
            MonthList = new List<Month>();
            YearList = new List<Year>();
            PopulateLists();
            GenerateCalendar(DateTime.Now);
        }
        public void GenerateCalendar(DateTime date)
        {
            //Debug.WriteLine("xxxx");
            // Get the Date, then compute for the first and last day of that month
            DateTime selectedDate = date;
            DateTime firstMonthDay = new DateTime(selectedDate.Year, selectedDate.Month, 1);
            DateTime lastMonthDay = new DateTime(selectedDate.Year, selectedDate.Month, DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month));

            // Get the First and Last Week Number
            int firstWeekNumberOfMonth = GetWeekNumber(firstMonthDay);
            int lastWeekNumberOfMonth = GetWeekNumber(lastMonthDay);

            // Add +1 Week if there is a hanging Sunday (Not considered as week by itself)
            if (lastMonthDay.DayOfWeek == DayOfWeek.Sunday)
                lastWeekNumberOfMonth += 1;

            // Initialize Row
            int currentRow = 1;

            // Refresh Calendar
            Calendar.Clear();

            // Insert Day Titles First (Sun-Sat) in Calendar
            InitializeDayTitles();

            // If Starting Week Number is from Last Year
            if (firstWeekNumberOfMonth == 52)
            {
                PrintDaysOfWeek(currentRow, firstMonthDay.Month, firstMonthDay.AddDays(-1), firstWeekNumberOfMonth);
                currentRow++;
                firstWeekNumberOfMonth = 1;
            }
            // If Starting Week is 53, set it to 52
            else if (firstWeekNumberOfMonth == 53)
            {
                PrintDaysOfWeek(currentRow, firstMonthDay.Month, firstMonthDay.AddDays(-1), firstWeekNumberOfMonth - 1);
                currentRow++;
                firstWeekNumberOfMonth = 1;
            }

            // Loop through Remaining Week Numbers
            for (int weekNumber = firstWeekNumberOfMonth; weekNumber <= lastWeekNumberOfMonth; weekNumber++)
            {
                PrintDaysOfWeek(currentRow, firstMonthDay.Month, selectedDate, weekNumber);
                currentRow++;
            }
            OnPropertyChanged("Calendar");
        }

        public void PrintDaysOfWeek(int currentRow, int currentMonth, DateTime selectedDate, int weekNumber)
        {
            // Get an Array of Days using the Week Number and Year
            DateTime[] daysInWeek = GetDaysOfWeek(selectedDate.Year, weekNumber);

            // Loop through all Days
            foreach (DateTime days in daysInWeek)
            {
                Calendar.Add(new CalendarModel(){ 
                    DayNumber=days.Day.ToString(),
                    Opacity = (days.Month != currentMonth) ? 0.8 : 1,
                    FontAttributes = (days.Month != currentMonth) ? FontAttributes.None : FontAttributes.Bold,
                    BorderColor = Color.Black,
                    CornerRadius = 2,
                    FontSize = 16,
                    Margin = 2,
                    Date = days,
                });

            }
        }

        // Function to initially Add Day Titles to Calendar Model
        public void InitializeDayTitles()
        {
            string[] weekDays = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
            for (int i = 0; i < weekDays.Length; i++)
            {
                Calendar.Add(new CalendarModel(){ 
                    DayNumber = weekDays[i],
                    Opacity = 1.0,
                    FontAttributes = FontAttributes.Bold,
                    BorderColor = Color.Black,
                    CornerRadius = 2,
                    FontSize = 14,
                    Margin = 2,
                });
            }
        }

        public static DateTime[] GetDaysOfWeek(int Year, int WeekNumber)
        {
            DateTime start = new DateTime(Year, 1, 1).AddDays(7 * WeekNumber);
            start = start.AddDays(-((int)start.DayOfWeek));
            return Enumerable.Range(0, 7).Select(num => start.AddDays(num)).ToArray();
        }

        public static int GetWeekNumber(DateTime date)
        {
            // Using CultureInfo
            var day = (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            return CultureInfo.CurrentCulture.Calendar
                .GetWeekOfYear(date.AddDays(4 - (day == 0 ? 7 : day)), CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday);
        }
       
        public void PopulateLists()
        {
            MonthList.Clear();
            MonthList.Add(new Month() { MonthName = "January"});
            MonthList.Add(new Month() { MonthName = "February" });
            MonthList.Add(new Month() { MonthName = "March" });
            MonthList.Add(new Month() { MonthName = "April" });
            MonthList.Add(new Month() { MonthName = "May" });
            MonthList.Add(new Month() { MonthName = "June" });
            MonthList.Add(new Month() { MonthName = "July" });
            MonthList.Add(new Month() { MonthName = "August" });
            MonthList.Add(new Month() { MonthName = "September" });
            MonthList.Add(new Month() { MonthName = "October" });
            MonthList.Add(new Month() { MonthName = "November" });
            MonthList.Add(new Month() { MonthName = "December" });
            OnPropertyChanged("MonthList");

            YearList.Clear();
            YearList.Add(new Year() { YearNumber = "2018" });
            YearList.Add(new Year() { YearNumber = "2019" });
            YearList.Add(new Year() { YearNumber = "2020" });
            YearList.Add(new Year() { YearNumber = "2021" });
            YearList.Add(new Year() { YearNumber = "2022" });
            YearList.Add(new Year() { YearNumber = "2023" });
            YearList.Add(new Year() { YearNumber = "2024" });
            OnPropertyChanged("YearList");
        }

        #endregion
        // Triggers when a property is changed in viewmodel
        #region INotifyPropertyChanged implementation
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class Month
    {
        public string MonthName { get; set; }
    }

    public class Year
    { 
        public string YearNumber { get; set;  }
    }
}
