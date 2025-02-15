using System.Security.Cryptography;
using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Users;
using DataPoints.Domain.Entities.Audit;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Audit;
using DataPoints.Domain.Repositories.Main;

namespace DataPoints.Application.Members.Commands.Wallets.Insert;

public class WalletInsertCommandHandler : ICommandHandler<WalletInsertCommand>
{
    
    private readonly IWalletRepository _walletRepository;
    private readonly IWalletLogRepository _walletLogRepository;

    private readonly IUserRepository _userRepository;
    
    public WalletInsertCommandHandler(IWalletRepository walletRepository, IWalletLogRepository walletLogRepository, IUserRepository userRepository)
    {
        _walletRepository = walletRepository;
        _walletLogRepository = walletLogRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(WalletInsertCommand request, CancellationToken cancellationToken)
    {
        if(!await _userRepository.Exists(request.IdUser))
            throw new UserNotFoundException(request.IdUser);
        
        using var rsa = new RSACryptoServiceProvider(2048);
        var chavePublica = rsa.ExportParameters(false);

        byte[] chavePublicaBytes = chavePublica.Modulus!;

        string chavePublicaBase64 = Convert.ToBase64String(chavePublicaBytes);

        var wallet = new WalletEntity
        {
            IdUser = request.IdUser,
            PublicKey = chavePublicaBase64,
        };
        
        await _walletRepository.Add(wallet, request.LoggedPerson.Name, cancellationToken);
        await _walletLogRepository.Add(new WalletLogEntity(wallet), request.LoggedPerson.Name, cancellationToken);
    }
}
