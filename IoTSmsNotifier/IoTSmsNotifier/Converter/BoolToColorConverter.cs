using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace IoTSmsNotifier.Converter
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Brush white = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff));
            Brush red = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0x00, 0x00));

            return ((bool)value) ? white : red;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
