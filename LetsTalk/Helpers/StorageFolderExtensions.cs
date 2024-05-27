using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace LetsTalk.Helpers
{
    public  static class StorageFolderExtensions
    {
        public static async Task<StorageFile> GetOrCreateFile(this StorageFolder storageFolder, string fileName)
        {
            try
            {
                var file = await storageFolder.TryGetItemAsync(fileName);
                if (file is null)
                {
                    return await storageFolder.CreateFileAsync(fileName);
                }
                return (StorageFile)file;

            }
            catch (Exception)
            {
                return await storageFolder.CreateFileAsync(fileName);
            }
        }


    }
}
