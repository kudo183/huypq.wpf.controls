using huypq.wpf.controls;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Demo
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private DateRangePickerViewModel dateRangePickerViewModel;

        public DateRangePickerViewModel DateRangePickerViewModel
        {
            get { return dateRangePickerViewModel; }
            set
            {
                if (dateRangePickerViewModel == value)
                    return;

                dateRangePickerViewModel = value;
                OnPropertyChanged();
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
