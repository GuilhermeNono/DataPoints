﻿using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;
using DataPoints.Infrastructure.Persistence.Main.Wallet.Transaction.Queries.FindAmountByWallet;
using DataPoints.Infrastructure.Persistence.Main.Wallet.Transaction.Queries.FindByBlockId;
using DataPoints.Infrastructure.Persistence.Main.Wallet.Transaction.Queries.FindByIdBlock;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Transaction;

public class WalletTransactionRepository : CrudRepository<WalletTransactionEntity, Guid>, IWalletTransactionRepository
{
    public WalletTransactionRepository(MainContext context) : base(context)
    {
    }

    public Task<decimal> FindAmountByWallet(Guid walletId)
    {
        var query = new FindAmountByWalletQuery(new FindAmountByWalletFilter(walletId));

        return QuerySingle(query);
    }

    public Task<IEnumerable<WalletTransactionEntity>> FindByIdBlock(Guid blockId)
    {
        var query = new FindByIdBlockQuery(new FindByIdBlockFilter(blockId));
        
        return Task.FromResult(Query(query));
    }

    public Task<IEnumerable<WalletTransactionEntity>> FindByBlockId(Guid blockId)
    {
        var query = new FindByBlockIdQuery(new FindByBlockIdFilter(blockId))
            .OrderBy(x => x.LastChangeAt, Sort.Desc);
        
        return Task.FromResult(Query(query));
    }
}
