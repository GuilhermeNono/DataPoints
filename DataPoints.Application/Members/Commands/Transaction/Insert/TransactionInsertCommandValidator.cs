using FluentValidation;

namespace DataPoints.Application.Members.Commands.Transaction.Insert;

public class TransactionInsertCommandValidator : AbstractValidator<TransactionInsertCommand>
{
    public TransactionInsertCommandValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0");

        RuleFor(x => x.ReceiverWallet)
            .NotEmpty();

        RuleFor(x => x.Signature)
            .NotEmpty();

        RuleFor(x => x.IdempotencyKey)
            .NotEqual(Guid.Empty)
            .WithMessage("Idempotency-Key header is required.");
    }
}