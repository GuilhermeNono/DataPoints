using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Contract.Wallet.GetBalance.Response;

namespace DataPoints.Application.Members.Queries.Wallet.GetBalance;

public record WalletGetBalanceFromUserQuery(string Identifier) : IQuery<BalanceResponse>
{
}