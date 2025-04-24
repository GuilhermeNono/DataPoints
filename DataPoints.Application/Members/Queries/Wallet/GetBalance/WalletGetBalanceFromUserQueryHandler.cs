using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Contract.Wallet.GetBalance;
using DataPoints.Contract.Wallet.GetBalance.Response;
using DataPoints.Crosscutting.Exceptions.Http.NotFound;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Users;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Wallet;
using DataPoints.Domain.Repositories.Main;

namespace DataPoints.Application.Members.Queries.Wallet.GetBalance;

public class WalletGetBalanceFromUserQueryHandler : IQueryHandler<WalletGetBalanceFromUserQuery, BalanceResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IWalletRepository _walletRepository;

    public WalletGetBalanceFromUserQueryHandler(IPersonRepository personRepository, IWalletRepository walletRepository)
    {
        _personRepository = personRepository;
        _walletRepository = walletRepository;
    }

    public async Task<BalanceResponse> Handle(WalletGetBalanceFromUserQuery request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.FindByNormalizedDocument(request.Identifier)
                   ?? throw new UserNotFoundException();

        var balance = await _walletRepository.FindBalanceByUser(person.Id);
        
        return new BalanceResponse(request.Identifier, balance);
    }
}