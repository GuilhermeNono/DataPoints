using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Commands.Block.Checkpoint.Clean;

public record BlockCleanCheckpointCommand(Guid ValidationId, LoggedPerson LoggedPerson) : ICommand
{
    
}