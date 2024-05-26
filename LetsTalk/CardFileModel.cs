using System;
using Windows.Storage;
using Windows.Storage.FileProperties;
using LetsTalk.Core.Contracts;
using LetsTalk.Core.Models;

namespace LetsTalk.Models
{
    class CardFileModel : ICardModelConverter
    {
        public string Title { get; set; }
        public string Image { get; set; }

        public ImageProperties ImageProperties { get; set; }
        public StorageFile ImageFile { get; set; }



        public CardFileModel(string title, string image, ImageProperties imageProperties, StorageFile imageFile)
        {
            Title = title;
            Image = image;
            ImageProperties = imageProperties;
            ImageFile = imageFile;
        }








        public CardsTableRowModel ToCardModel()
        {
            
        }
    }
}

