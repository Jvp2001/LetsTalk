using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using LetsTalk.Core.Contracts.Services;
using LetsTalk.Core.Helpers;
using Microsoft.Toolkit.Uwp.Helpers;


namespace LetsTalk.Core.Services
{
    public class FileService : IFileService
    {

        private StorageFolder installedDir = ApplicationData.Current.LocalFolder;
        public async Task<T> ReadAsync<T>(string folderPath, string fileName)
        {
            var path = Path.Combine(folderPath, fileName);
            StorageFile file;
            if (! await installedDir.FileExistsAsync(fileName))
            {
                file = await  installedDir.CreateFileAsync(fileName);
            }
            else
            {
                file =  await installedDir.GetFileAsync(fileName);
            }



            {
                var readTextAsync = await FileIO.ReadTextAsync(file);
                if (readTextAsync is null || readTextAsync == "")
                {
                    return default;
                }
                return Json.ToObject<T>(readTextAsync);
            }
        }
        public async Task SaveAsync<T>(string folderPath, string fileName, T content)
        {
            StorageFile file;

            if (! await installedDir.FileExistsAsync(fileName))
            {
                file = await  installedDir.CreateFileAsync(fileName);
            }
            else
            {
                file =  await installedDir.GetFileAsync(fileName);
            }

            await FileIO.WriteTextAsync(file, await Json.StringifyAsync(content));
        }


        public void Delete(string folderPath, string fileName)
        {

        }

    }
}
