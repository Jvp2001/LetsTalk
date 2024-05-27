using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using LetsTalk.Models;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LetsTalk.Views.Controls
{
    
    public sealed partial class BoardNameDialog : ContentDialog
    {
        private CardBoardModel board;
        public BoardNameDialog(CardBoardModel model)
        {
            this.InitializeComponent();
            this.DataContext = model;
            board = model;

        }

        public string BoardName { get; set; }


        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            BoardName = BoardNameTextBox.Text;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void BoardNameTextBlock_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            IsPrimaryButtonEnabled = !string.IsNullOrWhiteSpace(sender.Text);
            
        }

        private void BoardNameTextBox_OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Text.Length > 0 && e.Key == Windows.System.VirtualKey.Enter)
            {
                BoardName = textBox.Text;
                Hide();
            }
        }
    }
}
