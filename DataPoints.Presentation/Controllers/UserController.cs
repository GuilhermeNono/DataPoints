﻿using DataPoints.Application.Members.Queries.User.GetById;
using DataPoints.Contract.Controller.Users.Responses;
using DataPoints.Domain.Annotations;
using DataPoints.Domain.Enums;
using DataPoints.Presentation.Controllers.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataPoints.Presentation.Controllers;

[ApiRoute("users")]
public class UserController : ApiController
{
    public UserController(ISender sender, ILogger<UserController> logger) : base(sender, logger)
    {
    }

    [Protected(RoleProfile.Administrator)]
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<UserGetResponse>> GetUserByIdAsync(Guid id)
    {
        return Ok(await Sender.Send(new UserGetByIdQuery(id)));
    }
    
    
}
