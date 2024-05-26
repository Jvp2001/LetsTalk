using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LetsTalk.Contracts.Databases;
using LetsTalk.Helpers;

namespace LetsTalk.Models
{
    public sealed class CardBoardModel : IDatabaseName
    {
        [JsonIgnore]
        public string DatabaseName => $"{Name}.json";

        [JsonInclude]
        [JsonPropertyName("name")]
        public string Name { get; set; }


        [JsonInclude]
        [JsonPropertyName("cards")]
        public ObservableCollection<CardFileModel> Cards { get; set; } = new ObservableCollection<CardFileModel>();

        public CardBoardModel(string name)
        {
            Name = name;
        }

        [JsonConstructor]
        public CardBoardModel (string name, ObservableCollection<CardFileModel> cards)
        {

            Name = name;
            Cards = new ObservableCollection<CardFileModel>(cards);
        }

        public async Task SetupCards()
        {
           await Cards.SetupCardsAsync();
        }

        public int IndexOf(CardFileModel card)
        {
            return Cards.IndexOf(card);
        }
    }
}
