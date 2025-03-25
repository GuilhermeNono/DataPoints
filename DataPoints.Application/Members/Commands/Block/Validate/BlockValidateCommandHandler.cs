using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Domain.Repositories.Main;

namespace DataPoints.Application.Members.Commands.Block.Validate;

public class BlockValidateCommandHandler : ICommandHandler<BlockValidateCommand>
{
    
    private readonly IBlockRepository _blockRepository;

    public BlockValidateCommandHandler(IBlockRepository blockRepository)
    {
        _blockRepository = blockRepository;
    }

    public async Task Handle(BlockValidateCommand request, CancellationToken cancellationToken)
    {
        // TODO: Continuar a desenvolver a logica de validação por lote
        var blocks = await _blockRepository.FindNonValidated();
    }
}