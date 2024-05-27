using System;
using Windows.UI.Xaml.Data;
using LetsTalk.Models;
using Microsoft.Toolkit.Uwp.Input.GazeInteraction;
namespace LetsTalk.Convertors
{


    public class UserTypeToGazeInteractionConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is UserType userType))
            {
                return Interaction.Enabled;
            }

            switch (userType)
            {
                case UserType.Child:
                    return Interaction.Enabled;
                case UserType.Adult:
                    return Interaction.Disabled;
                default:
                    return Interaction.Enabled;
            }

        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (!(value is Interaction interaction))
            {
                return UserType.Child;
            }

            switch (interaction)
            {
                case Interaction.Enabled:
                    return UserType.Child;
                case Interaction.Disabled:
                    return UserType.Adult;
                case Interaction.Inherited:
                default:
                    return UserType.Child;
            }
        }
    }
}
