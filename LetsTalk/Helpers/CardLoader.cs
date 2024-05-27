using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using LetsTalk.Models;
namespace LetsTalk.Helpers
{
    public static class CardLoader
    {

        public static async Task SetupCardsAsync(this IEnumerable<CardFileModel>  cards)
        {
            foreach (var card in cards)
            {
                await SetupCardAsync(card);
            }
        }
        public static async Task SetupCardAsync(this CardFileModel card)
        {
            if (card.Image is null || card.Image == "")
            {
                return;
            }
            card.ImageFile = await StorageFile.GetFileFromPathAsync(card.Image ?? "");
            card.ImageProperties = await card.ImageFile.Properties.GetImagePropertiesAsync();
            if (card.Title == "")
            {
                card.Title = card.ImageFile.DisplayName;

            }
        }



    }
}
