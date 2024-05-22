using System.IO;
using System.Text;
using System.Threading.Tasks;
using LetsTalk.Core.Contracts.Services;
using LetsTalk.Core.Helpers;


namespace LetsTalk.Core.Services;

public class FileService : IFileService
{
    public T Read<T>(string folderPath, string fileName)
    {
        var path = Path.Combine(folderPath, fileName);
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "");
        }

        var json = File.ReadAllText(path);

        return Json.ToObject<T>(json);
    }

    public void Save<T>(string folderPath, string fileName, T content)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var fileContent = Json.Stringify(content);
        File.WriteAllText(Path.Combine(folderPath, fileName), fileContent, Encoding.UTF8);
    }

    public void Delete(string folderPath, string fileName)
    {
        if (fileName is not null && File.Exists(Path.Combine(folderPath, fileName)))
        {
            File.Delete(Path.Combine(folderPath, fileName));
        }
    }

    public async Task<T> ReadAsync<T>(string folderPath, string fileName)
    {
        var path = Path.Combine(folderPath, fileName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "");
        }

        var json = File.ReadAllText(path);
        if (json is null or "")
        {
            return default;
        }

        return Json.ToObject<T>(json);


    }

    public async Task SaveAsync<T>(string folderPath, string fileName, T content)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var fileContent = Json.Stringify(content);
        File.WriteAllText(Path.Combine(folderPath, fileName), fileContent, Encoding.UTF8);
    }
}
