using DataPoints.Application.Members.Commands.Transaction.Insert;
using DataPoints.Application.Members.Queries.Transaction.ByHash;
using DataPoints.Contract.Controller.Transaction.Insert.Request;
using DataPoints.Contract.Transaction.ByHash;
using DataPoints.Contract.Transaction.Insert;
using DataPoints.Domain.Annotations;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Interfaces;
using DataPoints.Presentation.Controllers.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataPoints.Presentation.Controllers;

[ApiRoute("transactions")]
public class TransactionController : ApiController
{
    public TransactionController(ISender sender, ILogger<IController> logger) : base(sender, logger)
    {
    }

    [Protected(RoleProfile.User)]
    [HttpPost]
    public async Task<ActionResult<TransactionInsertResponse>> InsertNewTransactionAsync([FromBody] TransactionInsertRequest request)
    {
        var transaction = await Sender.Send(new TransactionInsertCommand(request.ReceiverPublicKey, request.Amount, LoggedPerson));
        
        return Created($"api/transactions/{transaction.BlockId}", transaction);
    }
    
    [Protected(RoleProfile.User)]
    [HttpGet("{transactionHash}")]
    public async Task<ActionResult<IEnumerable<TransactionTreeResponse>>> GetTransactionTreeAsync(Guid transactionHash)
    {
        return Ok(await Sender.Send(new TransactionGetByBlockHashQuery(transactionHash)));
    }
}