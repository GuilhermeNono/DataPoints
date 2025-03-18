using DataPoints.Application.Members.Abstractions.Commands;

namespace DataPoints.Application.Members.Commands.Transaction.Block.Insert;

public class BlockInsertCommandHandler : ICommandHandler<BlockInsertCommand, string>
{
    public Task<string> Handle(BlockInsertCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}