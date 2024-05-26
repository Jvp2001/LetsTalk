using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetsTalk.Core.Contracts.Services;

public interface IAssetsManagerService
{

    public string GetAbsolutePath(string pathFromAssetsDirectory);

    public Task<List<string>> LoadAllImagesAsync(string folderPath, bool recursive, params string[] extensions);

}
