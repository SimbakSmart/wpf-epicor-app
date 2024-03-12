
using System.Globalization;
using System.Windows.Data;
using System;


namespace Epicor.App.Helpers
{
    public  class ResponsiveFontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double windowWidth = (double)value;
            return Math.Min(16.0, windowWidth / 100.0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
