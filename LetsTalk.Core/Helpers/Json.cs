

using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LetsTalk.Core.Helpers;

public static class Json
{

    public static T ToObject<T>(string value)
    {
        var options = CreateConvertor<T>();
        return JsonSerializer.Deserialize<T>(value, options);
    }

    public static string Stringify(object value)
    {
        var options = CreateConvertor<string>();
        return JsonSerializer.Serialize(value, options);
    }

   

    public static async Task<T> ToObjectAsync<T>(string value, CancellationToken token = default)
    {
        
        using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(value));

        var options = CreateConvertor<T>();
        return await JsonSerializer.DeserializeAsync<T>(memoryStream, cancellationToken: token, options: options);

    }

    private static JsonSerializerOptions CreateConvertor<T>()
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new ArrayOrObjectJsonConvertor<T>());
         return options;
    }

    public static async Task<string> StringifyAsync(object value)
    {
        using var memoryStream = new MemoryStream();
        var options = CreateConvertor<string>();  
        await JsonSerializer.SerializeAsync(memoryStream, value, options);
        return Encoding.UTF8.GetString(memoryStream.ToArray());
    }
}
