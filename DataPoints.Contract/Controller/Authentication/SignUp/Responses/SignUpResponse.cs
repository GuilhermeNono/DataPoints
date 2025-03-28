using DataPoints.Contract.Wallet.Private.Single;

namespace DataPoints.Contract.Controller.Authentication.SignUp.Responses;

public record SignUpResponse(Guid Id, WalletSinglePrivateInfoResponse PrivateInfo)
{
    
}
