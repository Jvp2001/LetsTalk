using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using LetsTalk.Core.Helpers;
using LetsTalk.Models;
using FileAttributes = System.IO.FileAttributes;

namespace LetsTalk.Helpers
{
    public static class JsonFileIo
    {
        public static async Task<T> DeserialiseFileAsync<T>(StorageFile file)
        {
            var contents = await FileIO.ReadTextAsync(file, UnicodeEncoding.Utf8);
            if (contents is null)
            {
                return default;
            }

            if (typeof(T) == typeof(CardBoardModel))
            {
                
                contents = contents.Substring(1, contents.Length - 2);
            }
            return await Json.ToObjectAsync<T>(contents);
        }

        public static async Task<T> DeserialiseFileAsync<T>(string fileName)
        {
            var file = await ApplicationData.Current.LocalFolder.TryGetItemAsync(fileName) as StorageFile;
            if (file is null)
            {
                return default;
            }


            var contents = await FileIO.ReadTextAsync(file);
            if (contents is null)
            {
                return default;
            }

            return await Json.ToObjectAsync<T>(contents);
        }

        public static async Task SerialiseFileAsync<T>(IStorageFile file, T content)
        {
            await FileIO.WriteTextAsync(file, Json.Stringify(content));
        }


        public static async Task SerialiseFileAsync<T>(string fileName, T content, bool append = false)
        {
            fileName = ValidateDatabaseName(fileName);

            await ApplicationData.Current.LocalFolder.TryGetItemAsync(fileName);
            var itemInfo = await ApplicationData.Current.LocalFolder.TryGetItemAsync(fileName);
            StorageFile localFile = null;

            if (itemInfo is null || itemInfo.Attributes == Windows.Storage.FileAttributes.Directory)
            {
                localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,
                    CreationCollisionOption.ReplaceExisting);
            }
            else
            {
                localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
            }

            try
            {
                if (append)
                {
                    await FileIO.AppendTextAsync(localFile, await Json.StringifyAsync(content));
                }
                else
                {
                    await FileIO.WriteTextAsync(localFile, await Json.StringifyAsync(content));
                }
            }
            catch (FileNotFoundException)
            {
                await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,
                    CreationCollisionOption.ReplaceExisting);
            }
        }

        private static string ValidateDatabaseName(string fileName)
        {
            if (fileName.EndsWith(".json"))
                return fileName;


            return Path.ChangeExtension(fileName, ".json");
        }
    }
}
