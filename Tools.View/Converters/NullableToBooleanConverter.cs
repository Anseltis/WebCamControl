using System;
using System.Globalization;
using System.Windows.Data;

namespace ESystems.WebCamControl.Tools.View.Converters
{
    [ValueConversion(typeof(object), typeof(bool))]
    public class NullableToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
