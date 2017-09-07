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
                    ip.ImageStream = null;
                }
                else
                {
                    var ms = new MemoryStream(File.ReadAllBytes(ip.FilePath));
                    ip.ImageStream = ms;
                }
            }
            catch { }
        }
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

        private bool isEditable;

        public bool IsEditable
        {
            get { return isEditable; }
            set
            {
                isEditable = value;
                if (brd != null)
                {
                    if (isEditable == true)
                    {
                        brd.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        brd.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }


        public ImagePicker()
        {
            InitializeComponent();

            ContextMenuOpening += ImagePicker_ContextMenuOpening;
        }

        private void ImagePicker_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (isEditable == false)
            {
                e.Handled = true;
            }
        }

        private void Rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //not working when use in datagrid column
            //if (isEditable == true)
            //{
            //    var contextMenu = rect.ContextMenu;
            //    contextMenu.PlacementTarget = img;
            //    contextMenu.IsOpen = true;
            //    e.Handled = true;
            //}
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

            return image;
        }
    }
}
