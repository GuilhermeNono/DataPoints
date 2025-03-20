using FluentValidation;

namespace DataPoints.Application.Members.Commands.Transaction.Insert;

public class TransactionInsertCommandValidator : AbstractValidator<TransactionInsertCommand>
{
    public TransactionInsertCommandValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0");
    }
}