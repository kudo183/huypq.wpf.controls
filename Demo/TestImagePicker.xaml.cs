using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Demo
{
    /// <summary>
    /// Interaction logic for TestImagePicker.xaml
    /// </summary>
    public partial class TestImagePicker : UserControl, INotifyPropertyChanged
    {
        private string file1;

        public string File1
        {
            get { return file1; }
            set
            {
                file1 = value;
                OnPropertyChanged();
            }
        }

        private string file2;

        public string File2
        {
            get { return file2; }
            set
            {
                file2 = value;
                OnPropertyChanged();
            }
        }

        private string file3;

        public string File3
        {
            get { return file3; }
            set
            {
                file3 = value;
                OnPropertyChanged();
            }
        }

        private Stream testStream;

        public Stream TestStream
        {
            get { return testStream; }
            set
            {
                testStream = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public TestImagePicker()
        {
            InitializeComponent();
            File2 = @"C:\Users\Public\Pictures\Sample Pictures\Koala.jpg";
            DataContext = this;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TestStream = new MemoryStream(File.ReadAllBytes(@"C:\Users\Public\Pictures\Sample Pictures\Chrysanthemum.jpg"));
        }
    }
}
