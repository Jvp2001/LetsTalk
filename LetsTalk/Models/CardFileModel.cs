using System;

namespace LetsTalk.Models
{
    class CardFileModel : ICardModelConverter
    {
        public string Title { get; set; }
        public string Image { get; set; }

        public ImageProperties ImageProperties { get; set; }
        

    }
}

