using FluentValidation;

namespace DataPoints.Application.Members.Queries.User.GetByDocument;

public class UserGetByDocumentQueryValidator : AbstractValidator<UserGetByDocumentQuery>
{
    public UserGetByDocumentQueryValidator()
    {
        RuleFor(x => x.Document)
            .NotEmpty()
            .MaximumLength(20);
    }
}
