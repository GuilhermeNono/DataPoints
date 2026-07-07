using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Contract.Wallet.Private;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Users;
using DataPoints.Domain.Entities.Audit;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Helpers;
using DataPoints.Domain.Repositories.Audit;
using DataPoints.Domain.Repositories.Main;

namespace DataPoints.Application.Members.Commands.Wallets.Insert;

public class WalletInsertCommandHandler : ICommandHandler<WalletInsertCommand, WalletPrivateKeyResponse>
{

    private readonly IWalletRepository _walletRepository;
    private readonly IChangeLogRepository _changeLogRepository;

    private readonly IUserRepository _userRepository;

    public WalletInsertCommandHandler(IWalletRepository walletRepository, IChangeLogRepository changeLogRepository, IUserRepository userRepository)
    {
        _walletRepository = walletRepository;
        _changeLogRepository = changeLogRepository;
        _userRepository = userRepository;
    }

    public async Task<WalletPrivateKeyResponse> Handle(WalletInsertCommand request, CancellationToken cancellationToken)
    {
        if(!await _userRepository.Exists(request.IdUser))
            throw new UserNotFoundException(request.IdUser);

        var groupOfKeys = SecurityHelper.CreateEcdsaKeys();

        var walletHash = SecurityHelper.CreateSha256Key(groupOfKeys.PublicKey);

        var wallet = new WalletEntity
        {
            IdUser = request.IdUser,
            Hash = walletHash.Hash,
            PublicKey = groupOfKeys.PublicKey,
        };

        await _walletRepository.Add(wallet, request.LoggedPerson.Name, cancellationToken);
        await _changeLogRepository.Add(ChangeLogEntity.For("Wlt_Wallets", wallet.Id, InternalOperation.C, wallet),
            request.LoggedPerson.Name, cancellationToken);

        return new WalletPrivateKeyResponse(groupOfKeys.PrivateKey);
    }
}
