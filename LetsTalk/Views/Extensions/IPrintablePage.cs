using System;
using Windows.Graphics.Printing;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Printing;
using Windows.UI.Xaml.Shapes;

namespace LetsTalk.Views.Extensions
{
    public interface IPrintablePage
    {
        PrintManager PrintManager { get; set; }
        PrintDocument PrintDocument { get; set; }
        IPrintDocumentSource PrintDocumentSource { get; set; }

        Rectangle RectangleToPrint { get; set; }
    }

    public static class IPrintablePageExtensions
    {
        public static void SetupForPrinting(this IPrintablePage printablePage)
        {
            printablePage.PrintManager = PrintManager.GetForCurrentView();
            printablePage.PrintManager.PrintTaskRequested +=
                (sender, args) => OnPrintTaskRequested(printablePage, sender, args);

            printablePage.PrintDocument = new PrintDocument();
            printablePage.PrintDocumentSource = printablePage.PrintDocument.DocumentSource;
            printablePage.PrintDocument.Paginate += (sender, e) => OnPaginate(printablePage, sender, e);
            printablePage.PrintDocument.GetPreviewPage += (sender, e) => OnGetPreviewPage(printablePage, sender, e);
            printablePage.PrintDocument.AddPages += (sender, e) => OnAddPages(printablePage, sender, e);
        }


        #region Showing the Print Dialog

        public static async void Print(this IPrintablePage printablePage)
        {
            if (PrintManager.IsSupported())
            {
                try
                {
                    await PrintManager.ShowPrintUIAsync();
                }
                catch
                {
                    // Printing cannot proceed at this time
                    var noPrintingDialog = new ContentDialog
                    {
                        Title = "Printing error",
                        Content = "Sorry, printing can't proceed at this time.",
                        PrimaryButtonText = "OK"
                    };

                    await noPrintingDialog.ShowAsync();
                }
            }
        }


        private static void OnPrintTaskRequested(IPrintablePage printablePage, PrintManager sender,
            PrintTaskRequestedEventArgs args)
        {
            // Create the PrintTask.
            PrintTask printTask = args.Request.CreatePrintTask("Print",
                sourceRequested => { sourceRequested.SetSource(printablePage.PrintDocumentSource); });

            // Handle PrintTask.Completed to catch failed print jobs.
            printTask.Completed += (s, e) => OnPrintTaskCompleted(printablePage, s, e);
        }

        #endregion


        #region Print Preview

        private static void OnGetPreviewPage(IPrintablePage printablePage, object sender, GetPreviewPageEventArgs e)
        {
            printablePage.PrintDocument.SetPreviewPage(1, printablePage.RectangleToPrint);
        }

        private static void OnPaginate(IPrintablePage printablePage, object sender, PaginateEventArgs e)
        {
            // I am only printing one rectangle.
            printablePage.PrintDocument.SetPreviewPageCount(1, PreviewPageCountType.Final);
        }

        #endregion


        #region Add Pages to Send to the Printer
        private static void OnAddPages(IPrintablePage printablePage, object sender, AddPagesEventArgs e)
        {
            printablePage.PrintDocument.AddPage(printablePage.RectangleToPrint);

            // Indicates that all pages have been provided.
            printablePage.PrintDocument.AddPagesComplete();
        }
#endregion

        #region Print Task Completed

        private static void OnPrintTaskCompleted(IPrintablePage printablePage, PrintTask sender, PrintTaskCompletedEventArgs args)
        {
            // Notify the user when the print operation fails.
            if (args.Completion == PrintTaskCompletion.Failed)
            {
                var dialog = new ContentDialog
                {
                    Title = "Printing error",
                    Content = "Sorry, failed to print.",
                    PrimaryButtonText = "OK"
                };

                dialog.ShowAsync();
            }
        }

#endregion
    }
}
