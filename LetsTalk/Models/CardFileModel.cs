using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using LetsTalk.Contracts.Databases;
using LetsTalk.Core.Helpers;
using LetsTalk.Helpers;
using Microsoft.Toolkit.Mvvm.ComponentModel;


namespace LetsTalk.Models
{
    public sealed class CardFileModel : ObservableObject, IDatabaseName
    {
        private StorageFile imageFile;
        private string title = "";

        [JsonIgnore] public string DatabaseName => "Cards.json";

        [JsonInclude]
        [JsonPropertyName("title")]
        public string Title
        {
            get => title;

            set => SetProperty(ref title, value);
        }

        [JsonInclude]
        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonIgnore] public ImageProperties ImageProperties { get; set; }

        [JsonIgnore]
        public StorageFile ImageFile
        {
            get => imageFile;

            set => SetProperty(ref imageFile, value);
        }


        [JsonConstructor]
        public CardFileModel(string title, string image)
        {
            Title = title;
            Image = image;
        }

        public CardFileModel(string image)
        {
            Image = image;
        }

        public CardFileModel(string title, string image, ImageProperties imageProperties, StorageFile imageFile)
        {
            Title = title;
            Image = image;
            ImageProperties = imageProperties;
            ImageFile = imageFile;
        }

        public CardFileModel()
        {
        }


        public async Task<BitmapImage> GetImageThumbnailAsync()
        {
            if (Image is null)
            {
                goto Default;
            }

            if (ImageFile is null)
            {
                await this.SetupCardAsync();
            }

            if (!(ImageFile is null))
            {
                using (IRandomAccessStream fileStream =
                       await ImageFile.GetThumbnailAsync(ThumbnailMode.SingleItem, 100,
                           ThumbnailOptions.ResizeThumbnail))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    await bitmapImage.SetSourceAsync(fileStream);
                    return bitmapImage;
                }
            }

            Default:
            return new BitmapImage(new Uri("ms-appx:///Assets/StoreLogo.png"));
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            title = Title.RemoveIfEndsWithAny(".png", ".jpg");
        }
    }

}
