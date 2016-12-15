using huypq.wpf.controls;
using System.Windows;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new MainWindowViewModel();
            vm.DateRangePickerViewModel = new DateRangePickerViewModel();
            DataContext = vm;
        }
    }
}
