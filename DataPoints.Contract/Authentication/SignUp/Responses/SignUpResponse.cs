using DataPoints.Contract.Wallet.Private.Single;

namespace DataPoints.Contract.Authentication.SignUp.Responses;

public record SignUpResponse(Guid Id, WalletSinglePrivateInfoResponse PrivateInfo)
{
    
}
