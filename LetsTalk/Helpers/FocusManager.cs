using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LetsTalk.Helpers
{
    public class FocusManager
    {
        public static readonly DependencyProperty FocusedElementProperty =
            DependencyProperty.RegisterAttached("FocusedElement", typeof(Control), typeof(FocusManager),
                new PropertyMetadata(null));

        

        public static Control GetFocusedElement(Control control)
        {
            return (Control)control.GetValue(FocusedElementProperty);
        }

        public static void SetFocusedElement(Control control, Control value)
        {
            control.SetValue(FocusedElementProperty, value);
        }
    }
}
