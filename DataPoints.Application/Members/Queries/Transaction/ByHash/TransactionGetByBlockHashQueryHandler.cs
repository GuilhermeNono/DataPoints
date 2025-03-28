using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Contract.Transaction.ByHash;
using DataPoints.Crosscutting.Exceptions.Http.NotFound;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Person;
using DataPoints.Crosscutting.Mapper.Responses;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Repositories.Main;

namespace DataPoints.Application.Members.Queries.Transaction.ByHash;

public class
    TransactionGetByBlockHashQueryHandler : IQueryHandler<TransactionGetByBlockHashQuery,
    IEnumerable<TransactionTreeResponse>>
{
    private readonly IWalletTransactionRepository _transactionRepository;
    private readonly IWalletRepository _walletRepository;
    private readonly IPersonRepository _personRepository;

    public TransactionGetByBlockHashQueryHandler(IWalletTransactionRepository transactionRepository,
        IPersonRepository personRepository, IWalletRepository walletRepository)
    {
        _transactionRepository = transactionRepository;
        _personRepository = personRepository;
        _walletRepository = walletRepository;
    }

    public async Task<IEnumerable<TransactionTreeResponse>> Handle(TransactionGetByBlockHashQuery request,
        CancellationToken cancellationToken)
    {
        var transactions = (await _transactionRepository.FindByBlockId(request.BlockId)).ToList();

        var result = new List<TransactionTreeResponse>();

        foreach (var transaction in transactions)
        {
            var walletFrom = await _walletRepository.FindById(transaction.IdWalletFrom)
                             ?? throw new WalletNotFoundException(transaction.IdWalletFrom);

            var walletTo = await _walletRepository.FindById(transaction.IdWalletTo)
                           ?? throw new WalletNotFoundException(transaction.IdWalletTo);

            var userFrom = await _personRepository.FindById(walletFrom.IdUser)
                           ?? throw new PersonNotFoundException();

            var userTo = await _personRepository.FindById(walletTo.IdUser)
                         ?? throw new PersonNotFoundException();

            result.Add(transaction.Map(userTo, userFrom));
        }

        return result;
    }
}