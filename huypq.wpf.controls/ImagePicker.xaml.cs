using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace huypq.wpf.controls
{
    /// <summary>
    /// Interaction logic for ImagePicker.xaml
    /// </summary>
    public partial class ImagePicker : UserControl
    {
        static OpenFileDialog ofd = new OpenFileDialog();

        #region FilePath
        public string FilePath
        {
            get { return (string)GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilePathProperty =
            DependencyProperty.Register("FilePath", typeof(string), typeof(ImagePicker), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnFilePathChanged));

        private static void OnFilePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ip = d as ImagePicker;

            if (ip == null)
            {
                return;
            }

            try
            {
                if (string.IsNullOrEmpty(ip.FilePath) == true)
                {
                    ip.img.Source = null;
                    ip.ID = 0;
                }
                else
                {
                    //ip.img.Source = new BitmapImage(new Uri(ip.FilePath));
                    var ms = new MemoryStream(File.ReadAllBytes(ip.FilePath));
                    ip.ImageStream = ms;
                }
            }
            catch { }
        }
        #endregion

        #region ID
        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(ImagePicker), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion

        #region ImageStream
        public Stream ImageStream
        {
            get { return (Stream)GetValue(ImageStreamProperty); }
            set { SetValue(ImageStreamProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageStream.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageStreamProperty =
            DependencyProperty.Register("ImageStream", typeof(Stream), typeof(ImagePicker), new PropertyMetadata(null, OnImageStreamChanged));

        private static void OnImageStreamChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ip = d as ImagePicker;

            if (ip == null)
            {
                return;
            }

            ip.img.Source = ImagePicker.LoadImage(ip.ImageStream);
        }
        #endregion

        public ImagePicker()
        {
            InitializeComponent();
        }

        private void Rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //var contextMenu = rect.ContextMenu;
            //contextMenu.PlacementTarget = img;
            //contextMenu.IsOpen = true;
        }

        private void Change_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ofd.ShowDialog() == true)
            {
                FilePath = ofd.FileName;
            }
        }

        private void Clear_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            FilePath = string.Empty;
        }

        public static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;

            using (var mem = new MemoryStream(imageData))
            {
                return ImagePicker.LoadImage(mem);
            }
        }

        public static BitmapImage LoadImage(Stream imageStream)
        {
            if (imageStream == null)
                return null;

            var image = new BitmapImage();

            imageStream.Position = 0;
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = null;
            image.StreamSource = imageStream;
            image.EndInit();

            image.Freeze();

            imageStream.Close();

            return image;
        }
    }
}
