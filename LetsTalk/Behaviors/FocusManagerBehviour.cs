using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

namespace LetsTalk.Behaviors
{
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

        private static void OnFocusedElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Control control)
            {
                control.Focus(FocusState.Programmatic);
            }
        }
    } 
}
