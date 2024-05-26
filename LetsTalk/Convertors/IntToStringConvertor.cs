using System;
using Windows.UI.Xaml.Data;
namespace LetsTalk.Convertors
{
    public class IntToStringConvertor : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int number)
            {
                return number.ToString();
            }
            return 0;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return
                int.TryParse(value as string, out var number) ? number : 0;
        }
    }
}
