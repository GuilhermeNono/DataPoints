﻿using System.Threading.Channels;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;
using DataPoints.Infrastructure.Persistence.Main.Wallet.Queries.FindBalanceByUser;
using DataPoints.Infrastructure.Persistence.Main.Wallet.Queries.FindByHash;
using DataPoints.Infrastructure.Persistence.Main.Wallet.Queries.FindByPublicKey;
using DataPoints.Infrastructure.Persistence.Main.Wallet.Queries.FindByUser;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet;

public class WalletRepository : CrudRepository<WalletEntity, Guid>, IWalletRepository
{
    public WalletRepository(MainContext context) : base(context)
    {
    }

    public Task<WalletEntity?> FindByPublicKey(string publicKey)
    {
        var query = new FindByPublicKeyQuery(new FindByPublicKeyFilter(publicKey));

        return QuerySingle(query);
    }

    public Task<WalletEntity?> FindByHash(string hash)
    {
        var query = new FindByHashQuery(new FindByHashFilter(hash));
        
        return QuerySingle(query);
    }

    public Task<WalletEntity?> FindByUser(Guid userId)
    {
        var query = new FindByUserQuery(new FindByUserFilter(userId));

        return QuerySingle(query);
    }

    public Task<decimal> FindBalanceByUser(Guid personId)
    {
        var query = new FindBalanceByUserQuery(new FindBalanceByUserFilter(personId));
        
        return QuerySingle(query);
    }
}
