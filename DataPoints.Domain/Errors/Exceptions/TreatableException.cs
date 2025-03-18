using System.Net;
using DataPoints.Domain.Errors.Abstractions;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;

namespace DataPoints.Domain.Errors.Exceptions;

public abstract class TreatableException : Exception, ITreatableException
{
    public virtual HttpStatusCode StatusCode { get; } = HttpStatusCode.InternalServerError;
    public string? Code { get; } = nameof(StatusCode);

    private readonly Lazy<Logger> _logger = new (() => new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger());

    protected Logger Logger => _logger.Value;

    protected TreatableException(string message) : base(message)
    {
    }

    public virtual IEnumerable<Error> ThrowHandledException()
    {
        Logger.Error(this, Message);
        return [new HttpError()];
    }
}
