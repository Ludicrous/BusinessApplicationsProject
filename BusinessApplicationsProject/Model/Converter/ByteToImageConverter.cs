using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

//STEVEN VANDEN BROUCKE 


namespace BusinessApplicationsProject.Model.Converter
{
    [ValueConversion(typeof(byte[]), typeof(Image))]
    public class ByteToImageConverter : IValueConverter
    {


        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte[] b = (byte[])value;

            try
            {
                BitmapImage biImg = new BitmapImage();
                MemoryStream ms = new MemoryStream(b);
                biImg.BeginInit();
                biImg.StreamSource = ms;
                biImg.EndInit();

                System.Windows.Media.ImageSource imgSrc = biImg as System.Windows.Media.ImageSource;

                return imgSrc;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
