using DataPoints.Application.Members.Queries.Wallet.GetBalance;
using DataPoints.Contract.Wallet.GetBalance.Request;
using DataPoints.Contract.Wallet.GetBalance.Response;
using DataPoints.Domain.Annotations;
using DataPoints.Domain.Interfaces;
using DataPoints.Presentation.Controllers.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataPoints.Presentation.Controllers;

[ApiRoute("wallets")]
public class WalletController : ApiController
{
    public WalletController(ISender sender, ILogger<IController> logger) : base(sender, logger)
    {
    }

    [AllowAnonymous]
    [HttpPost("balance")]
    public async Task<ActionResult<BalanceResponse>> GetBalanceAsync([FromBody]BalanceRequest request)
    {
        return await Sender.Send(new WalletGetBalanceFromUserQuery(request.Identifier));
    }
}