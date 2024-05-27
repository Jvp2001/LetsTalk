using System;
using System.Net.Mime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LetsTalk.Models;
using LetsTalk.Services;
using Microsoft.Toolkit.Uwp.Input.GazeInteraction;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace LetsTalk.Views.Controls
{

    public sealed partial class MenuButton : UserControl
    {

        private DependencyProperty goToProperty = DependencyProperty.Register(nameof(GoTo), typeof(string), typeof(MenuButton), new PropertyMetadata(string.Empty));

        public string GoTo
        {
            get => (string)GetValue(goToProperty);
            set => SetValue(goToProperty, value);
        }


        private DependencyProperty goToPayloadProperty = DependencyProperty.Register(nameof(GoToPayload), typeof(object), typeof(MenuButton), new PropertyMetadata(null));


        public object GoToPayload
        {
            get => GetValue(goToPayloadProperty);
            set => SetValue(goToPayloadProperty, value);
        }


        private DependencyProperty textProperty = DependencyProperty.Register(nameof(MediaTypeNames.Text), typeof(string), typeof(MenuButton), new PropertyMetadata(string.Empty));

        public string Text
        {
            get => (string)GetValue(textProperty);
            set => SetValue(textProperty, value);
        }



        public Interaction InteractionStatus => App.Current.User == UserType.Child ? Interaction.Enabled : Interaction.Disabled;


        public MenuButton()
        {
            InitializeComponent();
        }


        private void MenuButton_OnClicked(object sender, RoutedEventArgs e)
        {


            if (GoTo is null || GoTo == string.Empty)
            {
                return;
            }

            if (GoTo.Contains(".") || GoTo.Contains(" ") || GoTo.Contains("Page"))
            {
                return;
            }

            var goToType = Type.GetType($"LetsTalk.Views.{GoTo}Page");
            if (goToType is null)
            {
                return;
            }

            NavigationService.Navigate(goToType, GoToPayload, null);


        }

        private void UserControl_Loaded()
        {

        }
    }
}
