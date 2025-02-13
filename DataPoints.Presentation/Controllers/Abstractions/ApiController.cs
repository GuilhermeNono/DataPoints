using DataPoints.Domain.Helpers;
using DataPoints.Domain.Interfaces;
using DataPoints.Domain.Objects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataPoints.Presentation.Controllers.Abstractions;

[ApiController]
public abstract class ApiController : ControllerBase, IController
{
    protected readonly ISender Sender;
    protected ILogger<IController> Logger;

    protected ApiController(ISender sender, ILogger<IController> logger)
    {
        Sender = sender;
        Logger = logger;
    } 

    public LoggedPerson LoggedPerson { get; set; } 
}
