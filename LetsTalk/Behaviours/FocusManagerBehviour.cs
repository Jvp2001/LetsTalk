using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

namespace LetsTalk.Behaviours
{
    /// <summary>
    /// This allows you to set the focus on a <see cref="UIElement"/> in XAML (markup).
    /// </summary>
    /// <remarks>
    /// Inspired by WPF's <a href="https://github.com/dotnet/wpf/blob/main/src/Microsoft.DotNet.Wpf/src/PresentationCore/System/Windows/Input/FocusManager.cs">FocusManager</a> class.
    /// </remarks>
    public class FocusManagerBehaviour : Behavior<Control>

    {


        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotFocus += AssociatedObject_GotFocus;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotFocus -= AssociatedObject_GotFocus;
        }

        private void AssociatedObject_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is Control control)
            {
                control.Focus(FocusState.Programmatic);
            }
        }

        
    } 
}
