using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace huypq.wpf.controls
{
    /// <summary>
    /// Interaction logic for Base64Converter.xaml
    /// </summary>
    public partial class Base64Converter : UserControl
    {
        public Base64Converter()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (radioText.IsChecked == true)
                {
                    txtBase64.Text = Convert.ToBase64String(Encoding.UTF8.GetBytes(txtData.Text));
                }
                else
                {
                    txtBase64.Text = Convert.ToBase64String(ParseHexString(txtData.Text));
                }
            }
            catch (Exception ex)
            {
                txtBase64.Text = ex.ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (radioText.IsChecked == true)
                {
                    txtData.Text = Encoding.UTF8.GetString(Convert.FromBase64String(txtBase64.Text));
                }
                else
                {
                    txtData.Clear();
                    foreach (var item in Convert.FromBase64String(txtBase64.Text))
                    {
                        txtData.AppendText(item.ToString("X2"));
                    }
                }
            }
            catch (Exception ex)
            {
                txtData.Text = ex.ToString();
            }
        }

        public byte[] ParseHexString(string hex)
        {
            if (hex.Length % 2 != 0)
            {
                hex = hex.Insert(0, "0");
            }
            var result = new byte[hex.Length / 2];
            int resultIndex = 0;
            string s;
            for (int i = 0; i < hex.Length; i += 2)
            {
                s = hex.Substring(i, 2);
                result[resultIndex] = byte.Parse(s, System.Globalization.NumberStyles.HexNumber);
                resultIndex++;
            }
            return result;
        }

        private void CopyData_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtData.Text);
        }

        private void CopyBase64_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtBase64.Text);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (txtData.Text.StartsWith("0x") == true)
            {
                txtData.Text = txtData.Text.Replace("0x", "").Replace(" ", "").Replace(",", "");
            }
            else
            {
                var temp = txtData.Text.ToArray();
                var sb = new StringBuilder();
                for (int i = 0; i < temp.Length; i += 2)
                {
                    if (i > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append("0x");
                    sb.Append(temp[i]);
                    sb.Append(temp[i + 1]);
                }
                txtData.Text = sb.ToString();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (txtData.Text.Contains(" ") == true)
            {
                txtData.Text = txtData.Text.Replace(" ", "");
            }
            else
            {
                var temp = txtData.Text.ToArray();
                var sb = new StringBuilder();
                for (int i = 0; i < temp.Length; i += 2)
                {
                    if (i > 0)
                    {
                        sb.Append(" ");
                    }
                    sb.Append(temp[i]);
                    sb.Append(temp[i + 1]);
                }
                txtData.Text = sb.ToString();
            }
        }

        private void TxtHex_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtDec_TextChanged(object sender, TextChangedEventArgs e)
        {
            var number = int.Parse(txtDec.Text);
            txtHex.Text = Convert.ToString(number, 16);
            txtBin.Text = Convert.ToString(number, 2);
        }

        private void TxtBin_TextInput(object sender, TextCompositionEventArgs e)
        {
            var number = int.Parse(txtBin.Text);
            txtDec.Text = Convert.ToString(number, 10);
            txtHex.Text = Convert.ToString(number, 16);
        }

        private void TxtHex_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var number = Convert.ToInt32(txtHex.Text, 16);
                txtDec.Text = Convert.ToString(number, 10);
                txtBin.Text = Convert.ToString(number, 2);
            }
        }

        private void TxtDec_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var number = Convert.ToInt32(txtDec.Text, 10);
                txtHex.Text = Convert.ToString(number, 16);
                txtBin.Text = Convert.ToString(number, 2);
            }
        }

        private void TxtBin_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var number = Convert.ToInt32(txtBin.Text, 2);
                txtHex.Text = Convert.ToString(number, 16);
                txtDec.Text = Convert.ToString(number, 10);
            }
        }
    }
}
