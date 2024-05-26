using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using LetsTalk;
using LetsTalk.Convertors;
using LetsTalk.Views.Controls;
using Microsoft.Toolkit;
using Microsoft.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace LetsTalk.Views.Controls
{
    public class ValueChangedEventArgs : EventArgs
    {
        public int OldValue { get; set; }

        public int NewValue { get; set; }
    }

    public sealed partial class CreateABoardTextBox : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached(nameof(Text),
            typeof(string), typeof(CreateABoardTextBox), new PropertyMetadata(string.Empty));

        /// <summary>
        /// This is bound to <see cref="Label"/>
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }


        public static readonly DependencyProperty CurrentValueProperty =
            DependencyProperty.RegisterAttached(nameof(CurrentValue), typeof(int), typeof(CreateABoardTextBox),
                new PropertyMetadata(0));

        /// <summary>
        /// <p>This is the text property for the <see cref="Input"/> TextBox.</p>
        /// <p>This is done by using a Convertor class called <see cref="IntToStringConvertor"/>.
        /// </p>
        /// </summary>
        /// <remarks>
        /// This convertor allows for less code, due to not needing a separate property for the <see cref="Input"/> TextBox Text.
        /// Also, it allows the users of this control to write less code and gives a better user experience.
        /// </remarks>
        public int CurrentValue
        {
            get => (int)GetValue(CurrentValueProperty);
            set => SetValue(CurrentValueProperty, value);
        }


        public static readonly DependencyProperty IsValidProperty =
            DependencyProperty.RegisterAttached(nameof(IsValid), typeof(bool), typeof(CreateABoardTextBox),
                new PropertyMetadata(true));

        /// <summary>
        /// Indicates whether the number box is valid. If it is not, then the <see cref="Input"/>'s foreground colour will be set to red.
        /// </summary>

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }


        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(nameof(Minimum),
            typeof(int),
            typeof(CreateABoardTextBox), new PropertyMetadata(0));

        /// <summary>
        /// The minimum number allowed.
        /// </summary>
        public int Minimum
        {
            get => (int)GetValue(MinProperty);
            set => SetValue(MinProperty, value);
        }


        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(nameof(Maximum),
            typeof(int),
            typeof(CreateABoardTextBox), new PropertyMetadata(0));

        private int oldValue = 1;


        /// <summary>
        /// The maximum number allowed.
        /// </summary>
        public int Maximum
        {
            get => (int)GetValue(MaxProperty);
            set => SetValue(MaxProperty, value);
        }

        public event TypedEventHandler<TextBox, TextBoxTextChangingEventArgs> TextChanging
        {
            add => Input.TextChanging += value;
            remove => Input.TextChanging -= value;
        }

        public event TextChangedEventHandler TextChanged
        {
            add => Input.TextChanged += value;
            remove => Input.TextChanged -= value;
        }

        public CreateABoardTextBox()
        {
            InitializeComponent();

            CurrentValue = 1;

            Minimum = 0;
            Maximum = 3;

            RegisterPropertyChangedCallback(IsValidProperty, (sender, dp) =>
            {
                if (IsValid)
                {
                    // check if app is in light or dark mode
                    var isDarkMode = App.Current.RequestedTheme == ApplicationTheme.Dark;
                    Input.Foreground = new SolidColorBrush(isDarkMode ? Colors.White : Colors.Black);
                }
                else
                {
                    Input.Foreground = new SolidColorBrush(Colors.Red);
                }
            });


        }
     

        private void Input_OnBeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            // int previousValue = CurrentValue;
            // if (!sender.Text.IsNumeric())
            // {
            //     CurrentValue = previousValue;
            //     args.Cancel = true;
            // }


            args.Cancel = args.NewText == "" || !args.NewText.IsNumeric();
        }


        private void Input_OnKeyDown(object sender, KeyRoutedEventArgs e)
        {

        }
        private void Input_OnTextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (int.TryParse(sender.Text, out var value))
            {
                if (value >= Minimum && value <= Maximum)
                {
                    CurrentValue = value;
                    IsValid = true;
                }
                else
                {
                    IsValid = false;
                }
            }
            else
            {
                IsValid = false;
            }
        }
        private void Input_OnGotFocus(object sender, RoutedEventArgs e)
        {
            Input.SelectAll();
        }
    }
}
