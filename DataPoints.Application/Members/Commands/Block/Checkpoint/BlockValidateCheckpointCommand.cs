using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Commands.Block.Checkpoint;

public record BlockValidateCheckpointCommand(LoggedPerson LoggedPerson) : ICommand
{
    
}