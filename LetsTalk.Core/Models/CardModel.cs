
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace LetsTalk.Core.Models;




public class CardsTableRowModel
{




    // [Column("Name")]
    [JsonPropertyName("title")]
    public string Title { get;  }
    // [Column("Image")]
    [JsonPropertyName("image")]
    public string  Image { get; }

   

    public CardsTableRowModel(string image)
    {
        Image = image;
        Title = Path.GetFileNameWithoutExtension(image);
    }

    public CardsTableRowModel(string image, string title)
    {
        Image = image;
        Title = title;
    }
    public CardsTableRowModel()
    {
        Image = "";
        Title = "";
    }


}


