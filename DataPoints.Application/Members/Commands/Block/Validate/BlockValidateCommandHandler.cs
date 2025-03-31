using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Contract.Block.Validation;
using DataPoints.Crosscutting.Exceptions.Http.Internal;
using DataPoints.Domain.Helpers;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Domain.Security;

namespace DataPoints.Application.Members.Commands.Block.Validate;

public class BlockValidateCommandHandler : ICommandHandler<BlockValidateCommand, BlockValidationResponse>
{
    private readonly IBlockRepository _blockRepository;
    private readonly IWalletTransactionRepository _walletTransactionRepository;

    private const string GenesisBlock = "GenesisBlock";

    public BlockValidateCommandHandler(IBlockRepository blockRepository, IWalletTransactionRepository walletTransactionRepository)
    {
        _blockRepository = blockRepository;
        _walletTransactionRepository = walletTransactionRepository;
    }

    public async Task<BlockValidationResponse> Handle(BlockValidateCommand request, CancellationToken cancellationToken)
    {
        var block = await _blockRepository.FindById(request.BlockId)
                    ?? throw new BlockTransactionNotFoundException(request.BlockId);

        if (block.Hash.Equals(GenesisBlock, StringComparison.CurrentCultureIgnoreCase))
            return new BlockValidationResponse(true);
        
        var blockTransactions = (await _walletTransactionRepository.FindByIdBlock(block.Id)).ToList();

        if (blockTransactions.Count == 0)
            throw new BlockTransactionNotFoundException(block.Id);

        var calculateMerkleRootFromBlock =
            MerkleTree.ComputeRoot(blockTransactions.Select(x => x.TransactionSerialized).ToList());

        var calculateHash = BlockHelper.CalculateHash(block);

        if (!BlockHelper.IsValidBlockSignature(calculateHash, block.BlockSignature, block.PublicKey))
            return BlockValidationResponse.Invalid();
        
        var merkleRootIsCorrupted = !block.Hash.Equals(GenesisBlock, StringComparison.CurrentCultureIgnoreCase) &&
                                    !calculateMerkleRootFromBlock.Equals(block.MerkleRoot,
                                        StringComparison.CurrentCultureIgnoreCase);
        
        var hashIsCorrupted = !calculateHash.Equals(block.Hash, StringComparison.CurrentCultureIgnoreCase); 

        if (!merkleRootIsCorrupted && !hashIsCorrupted) return new BlockValidationResponse(block.IsValid);
        
        block.IsValid = false;
        await _blockRepository.Update(block, request.LoggedPerson.Name, cancellationToken);

        return new BlockValidationResponse(block.IsValid);
    }
}