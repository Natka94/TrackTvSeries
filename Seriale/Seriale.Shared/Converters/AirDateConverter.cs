using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Seriale.Converters
{
    class AirDateConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var date = (DateTime) value;
            return date.ToString("dd MMMM yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
