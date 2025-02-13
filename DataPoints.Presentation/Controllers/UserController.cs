﻿using DataPoints.Contract.Controller.Responses.Users;
using DataPoints.Presentation.Controllers.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataPoints.Presentation.Controllers;

[Route("api/users")]
public class UserController : ApiController
{
    public UserController(ISender sender, ILogger<UserController> logger) : base(sender, logger)
    {
    }

    [HttpGet]
    public async Task<ActionResult<UserGetResponse>> GetUsersAsync()
    {
        return Ok(new UserGetResponse());
    }
}
