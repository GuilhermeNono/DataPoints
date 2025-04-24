using DataPoints.Application.Members.Commands.Integration;
using DataPoints.Contract.Integration.Request;
using DataPoints.Domain.Annotations;
using DataPoints.Domain.Interfaces;
using DataPoints.Presentation.Controllers.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataPoints.Presentation.Controllers;

[ApiRoute("sso")]
public class SingleSignIOnController : ApiController
{
    public SingleSignIOnController(ISender sender, ILogger<IController> logger) : base(sender, logger)
    {
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> GetSsoLink([FromBody]IntegrationSsoRequest request)
    {
        return Ok(await Sender.Send(IntegrationSsoCommand.ToCommand(request)));
    }
}