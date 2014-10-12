using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Seriale.Converters
{
    class NextEpisodeDateConverter : IValueConverter
    {
        private string countDaysLeft(DateTime date)
        {
            TimeSpan diff = date - DateTime.Now ;
            return string.Format("({0}{1} left)",  diff.Days+1, diff.Days == 1 ? " day" : " days");
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var date = (DateTime) value;
            return date == default(DateTime) ? "Ended" : date.ToString("dd MMMM yyyy ") + countDaysLeft(date);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
