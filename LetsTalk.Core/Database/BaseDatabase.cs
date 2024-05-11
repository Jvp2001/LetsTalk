using System.Threading;
using LetsTalk.Core.Contracts.Services;


namespace LetsTalk.Core.Database;

public abstract class BaseDatabase
{
    // readonly Lazy<SQLiteAsyncConnection> sqliteDatabaseHolder;
    //
    // public const string DatabaseName = "IRPCommunicationProject.db3";
    // private const SQLiteOpenFlags OpenFlags = SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache;
    //
    // private SQLiteAsyncConnection DatabaseConnection => sqliteDatabaseHolder.Value;
    //
    //
    // public BaseDatabase(IAssetsManagerService assetsManagerService)
    // {
    //
    //
    //     var databasePath = assetsManagerService.GetAbsolutePath(DatabaseName);
    //     sqliteDatabaseHolder = new(() => new SQLiteAsyncConnection(databasePath, OpenFlags));
    // }
    //
    //
    // protected async Task<TReturn> Execute<TReturn, TDatabase>(Func<SQLiteAsyncConnection, Task<TReturn>> action, CancellationToken token, byte maxRetries = 10)
    // {
    //     var databaseConnection = await GetDataBaseConnection<TDatabase>();
    //
    //     var resiliencePipeline = new ResiliencePipelineBuilder<TReturn>()
    //         .AddRetry(new RetryStrategyOptions<TReturn>
    //         {
    //             MaxRetryAttempts = maxRetries,
    //             Delay = TimeSpan.FromMilliseconds(2),
    //             BackoffType = DelayBackoffType.Exponential
    //         }).Build();
    //     return await resiliencePipeline.ExecuteAsync(async _ =>  await action(databaseConnection), token);
    //
    // }
    // private async ValueTask<SQLiteAsyncConnection> GetDataBaseConnection<T>()
    // {
    //     if (DatabaseConnection.TableMappings.All(static x => x.MappedType == typeof(T)))
    //     {
    //         await DatabaseConnection.EnableWriteAheadLoggingAsync().ConfigureAwait(false);
    //         await DatabaseConnection.CreateTableAsync(typeof(T)).ConfigureAwait(false);
    //     }
    //
    //     return DatabaseConnection;
    // }


}
