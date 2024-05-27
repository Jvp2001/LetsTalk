using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

namespace LetsTalk.Views.Mixins
{
    /// <summary>
    /// Exports a single UWP page to a PDF file.
    /// </summary>
    public interface IPDFExportablePage
    {
        // This is so that the extension methods can access the page that is being exported.
        UIElement  Element { get; }
    }


    public static class IPDFExportablePageExtensions
    {
        public static void SetupPdfDocument(this IPDFExportablePage pdfExportablePage)
        {
            
            
        }

       
        #region Helper Methods


        public static async void SaveAndExport(this IPDFExportablePage pdfExportablePage, string fileName)
        {
            //Create a new PDF document
            PdfDocument document = new PdfDocument();
            //Initialize render to bitmap
            var logicalDpi = DisplayInformation.GetForCurrentView().LogicalDpi;
            var renderTargetBitmap = new RenderTargetBitmap();
            //Create a Bitmap from XAML page
            await renderTargetBitmap.RenderAsync(pdfExportablePage.Element);
            var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();
            //Save the XAML in bitmap image
            using (var stream = new Windows.Storage.Streams.InMemoryRandomAccessStream())
            {
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
                encoder.SetPixelData(
                    BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Premultiplied,
                    (uint)renderTargetBitmap.PixelWidth,
                    (uint)renderTargetBitmap.PixelHeight,
                    logicalDpi,
                    logicalDpi,
                    pixelBuffer.ToArray());
                await encoder.FlushAsync();
                //Load and draw the bitmap image in PDF
                PdfImage img = PdfImage.FromStream(stream.AsStream());
                document.PageSettings.Margins.All = 0;
                if (img.Width > img.Height)
                    document.PageSettings.Orientation = PdfPageOrientation.Landscape;
                else
                    document.PageSettings.Orientation = PdfPageOrientation.Portrait;
                document.PageSettings.Size = new SizeF(img.Width, img.Height);
                var page = document.Pages.Add();
                page.Graphics.DrawImage(img, new RectangleF(0, 0, img.Width, img.Height));
            }
            //Save the document
            MemoryStream docStream = new MemoryStream();
            document.Save(docStream);
            //Close the document 
            document.Close(true);
            pdfExportablePage.Save(docStream, fileName);

            
        }
        public static async void Save(this IPDFExportablePage pdfExportablePage, Stream stream, string filename)
        {
            stream.Position = 0;
            StorageFile stFile;
            if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.DefaultFileExtension = ".pdf";
                savePicker.SuggestedFileName = filename ?? "Output";
                savePicker.FileTypeChoices.Add("Adobe PDF Document", new List<string>() { ".pdf" });
                stFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                stFile = await local.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            }
            if (stFile != null)
            {
                Windows.Storage.Streams.IRandomAccessStream fileStream = await stFile.OpenAsync(FileAccessMode.ReadWrite);
                Stream st = fileStream.AsStreamForWrite();
                st.Write((stream as MemoryStream).ToArray(), 0, (int)stream.Length);
                st.Flush();
                st.Dispose();
                fileStream.Dispose();
                MessageDialog msgDialog = new MessageDialog("Do you want to view the Document?", "File created.");
                UICommand yesCmd = new UICommand("Yes");
                msgDialog.Commands.Add(yesCmd);
                UICommand noCmd = new UICommand("No");
                msgDialog.Commands.Add(noCmd);
                IUICommand cmd = await msgDialog.ShowAsync();
                if (cmd == yesCmd)
                {
                    // Launch the retrieved file
                    bool success = await Windows.System.Launcher.LaunchFileAsync(stFile);
                }
            }
        }
        #endregion

    }
}
