using DataPoints.Application.Members.Commands.Authentication.SignIn;
using DataPoints.Application.Members.Commands.Authentication.SignUp;
using DataPoints.Contract.Controller.Authentication.SignIn.Requests;
using DataPoints.Contract.Controller.Authentication.SignIn.Responses;
using DataPoints.Contract.Controller.Authentication.SignUp.Requests;
using DataPoints.Contract.Controller.Authentication.SignUp.Responses;
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
        return Ok(await Sender.Send(SignInCommand.ToCommand(user, LoggedPerson)));
    }
    
    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<ActionResult<SignUpResponse>> SignUpAsync([FromBody] SignUpRequest user)
    {
        var result = await Sender.Send(SignUpCommand.ToCommand(user, LoggedPerson));
        
        return Created("v1/auth/signin", result);
    }
}
