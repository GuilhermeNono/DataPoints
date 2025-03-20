using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace DataPoints.Application.Members.Behaviours;

public sealed class ValidationPipelineBehaviour<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        ValidationFailure[] errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validatorResult => validatorResult.Errors)
            .Where(validatorFailure => validatorFailure is not null)
            .Select(failure => failure)
            .Distinct()
            .ToArray();

        if (errors.Length != 0)
            throw new ValidationException(errors);

        return await next();
    }
}