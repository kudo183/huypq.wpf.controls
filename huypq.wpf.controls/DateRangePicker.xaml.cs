using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace huypq.wpf.controls
{
    /// <summary>
    /// Interaction logic for DateRangePicker.xaml
    /// </summary>
    public partial class DateRangePicker : UserControl
    {
        public DateRangePicker()
        {
            InitializeComponent();
        }

        private void DatePicker_CalendarOpened(object sender, RoutedEventArgs e)
        {
            var datepicker = sender as DatePicker;
            if (datepicker != null)
            {
                var popup = datepicker.Template.FindName(
                    "PART_Popup", datepicker) as Popup;
                if (popup != null && popup.Child is Calendar)
                {
                    ((Calendar)popup.Child).DisplayMode = CalendarMode.Year;
                }
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var datepicker = sender as DatePicker;
            if (datepicker == null || datepicker.SelectedDate == null)
                return;

            var vm = DataContext as DateRangePickerViewModel;
            if (vm == null)
                return;

            var date = datepicker.SelectedDate.Value;

            vm.DateFrom = new DateTime(date.Year, date.Month, 1);

            var temp = vm.DateFrom.AddMonths(1).Subtract(new TimeSpan(1, 0, 0, 0));

            vm.DateTo = new DateTime(date.Year, date.Month, temp.Day);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null || comboBox.SelectedItem == null)
                return;

            var vm = DataContext as DateRangePickerViewModel;
            if (vm == null)
                return;

            var year = (int)comboBox.SelectedItem;
            vm.DateFrom = new DateTime(year, 1, 1);
            vm.DateTo = new DateTime(year, 12, 31);
        }
    }
}
