using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace huypq.wpf.controls
{
    public class DateRangePickerViewModel : INotifyPropertyChanged
    {
        private void Log(string msg, [CallerMemberName] string caller=null)
        {
            Console.WriteLine(string.Format("{0}: {1}", caller, msg));
        }

        public DateRangePickerViewModel()
        {
            Date = DateTime.Now.Date;
            var year = Date.Year;
            Nams = new List<int>() { year, year - 1, year - 2, year - 3, year - 4, year - 5 };
        }

        public List<int> Nams { get; set; }

        private DateTime dateFrom;

        public DateTime DateFrom
        {
            get { return dateFrom; }
            set
            {
                if (dateFrom == value)
                    return;

                dateFrom = value;
                OnPropertyChanged();
                Log(dateFrom.ToShortDateString());
            }
        }

        private DateTime dateTo;

        public DateTime DateTo
        {
            get { return dateTo; }
            set
            {
                if (dateTo == value)
                    return;

                dateTo = value;
                OnPropertyChanged();
                Log(dateTo.ToShortDateString());
            }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date == value)
                    return;

                date = value;
                OnPropertyChanged();

                DateFrom = DateTo = date;
                Log(date.ToShortDateString());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
