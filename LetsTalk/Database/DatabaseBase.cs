using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;
using LetsTalk.Contracts.Databases;
using LetsTalk.Helpers;

namespace LetsTalk.Database
{

    /// <summary>
    /// The base class for all databases.
    /// </summary>
    /// <remarks>
    /// I am using JSON as my database format.
    /// Terms:
    /// A database, in my application, is either a single JSON file or a folder of JSON files.
    /// </remarks>
    public abstract class DatabaseBase<TModel> where TModel : class, IDatabaseName
    {
        public const string DatabaseFolderName = "Databases";

        /// <summary>
        /// This allows each database operation to be change its behaviour depending on if the database is a folder or a file.
        /// </summary>
        public virtual bool IsEachModelItsOwnTable => false;


        public abstract string DatabasePath { get; }

        #region Database Management

        public virtual async Task DropTableAsync(TModel model)
        {
            var tablePath = GetTablePath(model);

            if (IsEachModelItsOwnTable)
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(tablePath);
                await file.DeleteAsync();
            }
        }
        public virtual async Task UpdateAsync(TModel card, CancellationToken token)
        {
            var cards = new TModel[] { card };

            await JsonFileIo.SerialiseFileAsync(GetTablePath(card), cards);
        }


        public virtual async Task UpdateAllUniqueAsync(IEnumerable<TModel> cards, CancellationToken token)

        {
            var set = cards.ToImmutableHashSet();

            if (IsEachModelItsOwnTable)
            {
                foreach (TModel model in set.AsParallel())
                {
                    await UpdateAsync(model, token);
                }
            }
            else
            {
                var name = GetTablePath(set.First());
                if (name is null || name == "")
                {
                    return;
                }
                await JsonFileIo.SerialiseFileAsync(name, set.ToArray());
                
            }
        }


        public virtual async Task UpdateAllAsync(IEnumerable<TModel> cards, CancellationToken token)
        {
            var content = cards as TModel[] ?? cards.ToArray();

            if (IsEachModelItsOwnTable)
            {
                foreach (TModel model in content.AsParallel())
                {
                    await UpdateAsync(model, token);
                }
            }
            else
            {
                var databaseName = content.First();

                var validatedTableName = GetTablePath(databaseName);
                

                    await JsonFileIo.SerialiseFileAsync(validatedTableName, content, false);
                
            }
        }

        public virtual async Task InsertAllUniqueAsync(IEnumerable<TModel> cards, CancellationToken token)
        {
            var set = cards.ToImmutableHashSet();

            if (IsEachModelItsOwnTable)
            {
                foreach (TModel model in set)
                {
                    await InsertAsync(model, token);
                }
            }
            else
            {
                var currentCards = await JsonFileIo.DeserialiseFileAsync<TModel[]>(DatabasePath) ?? new TModel[] { };
                var newCards = currentCards.Concat(set).ToImmutableHashSet().ToArray();
                await JsonFileIo.SerialiseFileAsync(DatabasePath, newCards);
            }
        }

        public virtual async Task InsertAsync(TModel card, CancellationToken token)
        {
            var cards = new TModel[] { card };

            await JsonFileIo.SerialiseFileAsync(GetTablePath(card), cards, true);
        }


        public virtual async Task<ObservableCollection<TModel>> Get()
        {
            if (IsEachModelItsOwnTable)
            {
                var files = await (await ApplicationData.Current.LocalFolder.GetFolderAsync(DatabasePath))
                    .CreateFileQueryWithOptions(new QueryOptions(CommonFileQuery.DefaultQuery, new[] { ".json" })).GetFilesAsync();
                        //Multiple threads can be used to load the data
                        var boards = new ConcurrentBag<TModel>();

                        foreach (StorageFile storageFile in files.AsParallel())
                        {
                            TModel board = await JsonFileIo.DeserialiseFileAsync<TModel>(storageFile);

                            boards.Add(board);
                        }

                        return new ObservableCollection<TModel>(boards.ToArray());
            }

            return new ObservableCollection<TModel>(await JsonFileIo.DeserialiseFileAsync<TModel[]>(DatabasePath) ??
                                                    new TModel[] { });
        }

        protected string GetTablePath(TModel model)
        {

            return $@"{DatabasePath}\{model.DatabaseName}";
        }

#endregion
    }
}
