using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LoginChuchu.Models
{
    public partial class CalendarModel
    {
        public int Id { get; set; }
        public string DayNumber { get; set; }
        public double Opacity { get; set; }
        public FontAttributes FontAttributes { get; set; }
        public Color BorderColor { get; set; }
        public ushort CornerRadius { get; set; }
        public ushort FontSize { get; set; }
        public ushort Margin { get; set; }
        public DateTime? Date { get; set; } = null;
    }
}
