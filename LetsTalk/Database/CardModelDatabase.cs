// using System.Collections.ObjectModel;
// using LetsTalk.Core.Contracts.Services;
// using LetsTalk.Core.Database;
// using LetsTalk.Core.Models;
//
// namespace LetsTalk.Database;
//
// public class CardModelDatabase(IAssetsManagerService assetsManagerService) : BaseDatabase(assetsManagerService)
// {
//
//
//     public async Task<ObservableCollection<CardsTableRowModel>> Get(CancellationToken token)
//     {
//         return await Execute<ObservableCollection<CardsTableRowModel>, CardsTableRowModel>(async database =>
//         {
//             var cards = await database.Table<CardsTableRowModel>().ToListAsync();
//
//             return new ObservableCollection<CardsTableRowModel>(cards);
//
//         }, token);
//     }
//
//     public async Task<CardsTableRowModel> GetCardById(ulong id, CancellationToken token)
//     {
//         return await Execute<CardsTableRowModel, CardsTableRowModel>(async database =>
//         {
//             var card = await database.Table<CardsTableRowModel>().Where(x => x.Id == id).FirstOrDefaultAsync();
//             return card;
//         }, token);
//     }
//
//     public async Task<int> SaveCard(CardsTableRowModel card, CancellationToken token)
//     {
//         return await Execute<int, CardsTableRowModel>(async database =>
//         {
//             if (card.Id != 0)
//             {
//                 return await database.UpdateAsync(card);
//             }
//             return await database.InsertAsync(card);
//         }, token);
//     }
//
//     public async Task<int> DeleteCard(CardsTableRowModel card, CancellationToken token)
//     {
//         return await Execute<int, CardsTableRowModel>(async database =>
//         {
//             return await database.DeleteAsync(card);
//         }, token);
//     }
//
//     public async Task<bool> IsEmpty(CancellationToken token)
//     {
//         return await Execute<bool, CardsTableRowModel>(async database => await database.Table<CardsTableRowModel>().CountAsync() == 0, token);
//     }
//
//
//     public async Task<int> InsertAllUniqueCards(IEnumerable<CardsTableRowModel> cards, CancellationToken token)
//     {
//         return await Execute<int, CardsTableRowModel>(async database =>
//         {
//             var existingCards = new HashSet<CardsTableRowModel>(await database.Table<CardsTableRowModel>().ToListAsync());
//             return await database.InsertAllAsync(cards.Intersect(cards));
//         }, token);
//     }
//
// //TODO: Fix this crash tomorrow morning.
//     public async Task<int> UpdateAll(IEnumerable<CardsTableRowModel> cards, CancellationToken token) =>
//         await Execute<int, CardsTableRowModel>(async database =>
//             await database.InsertAllAsync(cards), token);
//
// }


using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LetsTalk.Core.Contracts;
using LetsTalk.Core.Contracts.Services;
using LetsTalk.Core.Models;
using LetsTalk.Database;
using LetsTalk.Helpers;
using LetsTalk.Models;

namespace LetsTalk.Database
{
}

public class CardModelDatabase : DatabaseBase<CardFileModel>
{
    

    public override string DatabasePath => $"{DatabaseFolderName}\\";


    protected override async Task OnDataFetched(IEnumerable<CardFileModel> data)
    {
        await data.SetupCardsAsync();
    }
}
