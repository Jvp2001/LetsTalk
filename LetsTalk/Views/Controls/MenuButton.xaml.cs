using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using LetsTalk.Services;

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

        public MenuButton()
        {
            this.InitializeComponent();
        }

        private void MenuButton_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {

            if (GoTo is null || GoTo == string.Empty)
            {
                return;
            }

            if (GoTo.Contains(".") || GoTo.Contains(" ") || GoTo.Contains("ViewModel"))
            {
                return;
            }

            var goToType = Type.GetType($"LetsTalk.ViewModels.{GoTo}ViewModel");
            if(goToType is null )
            {
                return;
            }

            NavigationService.Navigate( goToType, GoToPayload, null); 

        
        }
    }
}
