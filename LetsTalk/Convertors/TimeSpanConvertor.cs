using System;
using Windows.UI.Xaml.Data;
namespace LetsTalk.Convertors
{
    /// <summary>
    /// Converts an int into a TimeSpan of seconds.
    /// </summary>
    public class TimeSpanConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int seconds)
            {
                return TimeSpan.FromSeconds(seconds);
            }

            return TimeSpan.Zero;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is TimeSpan timeSpan)
            {
                return timeSpan.Seconds;
            }

            return 0;

        }
    }
}
