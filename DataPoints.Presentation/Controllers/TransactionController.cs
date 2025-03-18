using DataPoints.Application.Members.Commands.Transaction.Insert;
using DataPoints.Contract.Controller.Transaction.Insert.Request;
using DataPoints.Domain.Annotations;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Interfaces;
using DataPoints.Presentation.Controllers.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataPoints.Presentation.Controllers;

[Route("api/transactions")]
public class TransactionController : ApiController
{
    public TransactionController(ISender sender, ILogger<IController> logger) : base(sender, logger)
    {
    }

    [Protected(RoleProfile.User)]
    [HttpPost]
    public async Task<IActionResult> InsertNewTransactionAsync([FromBody] TransactionInsertRequest request)
    {
        var transactionId = await Sender.Send(new TransactionInsertCommand(request.ReceiverPublicKey, request.Amount, LoggedPerson));
        
        return Created($"api/transactions/{transactionId}", null);
    }
}