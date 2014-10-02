using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Seriale.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, string language)
        {
            var boolean = (bool)value;

            return boolean ? "Visible" : "Collapsed"; 
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, string language)
        {
            throw new System.NotImplementedException();
        }
    }
}