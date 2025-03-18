using DataPoints.Application.Members.Abstractions.Commands;

namespace DataPoints.Application.Members.Commands.Transaction.Block.Insert;

public record BlockInsertCommand() : ICommand<string>
{
    
}