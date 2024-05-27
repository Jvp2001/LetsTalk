using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using LetsTalk.Models;

namespace LetsTalk.Convertors
{
    public class UserTypeToVisibilityConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is UserType userType))
            {
                return Windows.UI.Xaml.Visibility.Collapsed;
            }

            switch (userType)
            {
                case UserType.Adult:
                    return Windows.UI.Xaml.Visibility.Visible;
                case UserType.Child:
                    return Windows.UI.Xaml.Visibility.Collapsed;
                default:
                    return Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (!(value is Windows.UI.Xaml.Visibility visibility))
            {
                return UserType.Child;
            }

            switch (visibility)
            {
                case Windows.UI.Xaml.Visibility.Visible:
                    return UserType.Child;
                case Windows.UI.Xaml.Visibility.Collapsed:
                    return UserType.Adult;
                default:
                    return UserType.Child;
            }
        }

    }
}
