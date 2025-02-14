using DataPoints.Application.Members.Commands.Authentication.SignIn;
using DataPoints.Contract.Controller.Authentication.SignIn.Requests;
using DataPoints.Contract.Controller.Authentication.SignIn.Responses;
using DataPoints.Domain.Interfaces;
using DataPoints.Presentation.Controllers.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataPoints.Presentation.Controllers;

[Route("v1/auth")]
public class AuthenticationController : ApiController
{
    public AuthenticationController(ISender sender, ILogger<IController> logger) : base(sender, logger)
    {
    }

    [AllowAnonymous]
    [HttpPost("signin")]
    public async Task<ActionResult<SignInResponse>> SignInAsync([FromBody] SignInRequest user)
    {
        return Ok(await Sender.Send(new SignInCommand(user.Email, user.Password, LoggedPerson)));
    }
}