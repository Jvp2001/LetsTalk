using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using LetsTalk.Core.Contracts.Services;

namespace LetsTalk.Services
{
    internal class AssetsManagerService : IAssetsManagerService
    {
        private string AssetPath => Windows.ApplicationModel.Package.Current.InstalledLocation.Path;

        public string GetAbsolutePath(string pathFromAssetsDirectory) =>
            Path.Combine(AssetPath, "Assets", pathFromAssetsDirectory);

        public async Task<List<string>> LoadAllImagesAsync(string folderPath, bool recursive,
            params string[] extensions)
        {
            var folder = await StorageFolder.GetFolderFromPathAsync(GetAbsolutePath(folderPath));
            var files = Directory.GetFiles(folder.Path, "*.*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                .Select(path => new FileInfo(path))
                .ToList();

            return
            (
                from file in files
                where extensions.Contains(file.Extension)
                select file.FullName
            ).ToList();
        }
    }
}
