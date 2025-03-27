using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Contract.Block.Validation;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Commands.Block.Validate;

public record BlockValidateCommand(Guid BlockId, LoggedPerson LoggedPerson) : ICommand<BlockValidationResponse>
{
    
}