using LetsTalk.ViewModels;

using Windows.UI.Xaml.Controls;

namespace LetsTalk.Views
{

    /// <summary>
    /// I use this instead of a splash screen. A splash screen can be set to optional in the app manifest. It will not be used if the app starts quickly.
    /// </summary>
    public sealed partial class WelcomePage : Page
    {
        public WelcomeViewModel ViewModel { get; } = new WelcomeViewModel();

        public WelcomePage()
        {
            InitializeComponent();
        }
    }
}
