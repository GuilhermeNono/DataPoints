namespace DataPoints.Domain.Security;

public class BlockChain 
{
    public List<Block> Chain { get; set; }
    public int Difficulty { get; set; } = 1;

    public BlockChain()
    {
        Chain = [CreateGenesisBlock()];
    }

    public void AddBlock(Block block)
    {
        block.PreviousHash = GetLatestBlock().Hash;
        block.MineBlock(Difficulty);
        Chain.Add(block);
    }

    public string GetChain()
    {
        var chainWithoutGenesisBlock = Chain.Where(x => x.Index != 0).ToList();
        return string.Join("\n", chainWithoutGenesisBlock.Select(b => b.Data));
    }
    
    private Block CreateGenesisBlock()
    {
        return new Block(0, [], uint.MinValue.ToString());
    }

    public Block GetLatestBlock()
    {
        return Chain[^1];
    }

    public bool IsValid()
    {
        for (int i = 1; i < Chain.Count - 1; i++)
        {
            Block currentBlock = Chain[i];
            Block previousBlock = Chain[i - 1];
            
            if(currentBlock.Hash != currentBlock.CalculateHash())
                return false;

            if (currentBlock.PreviousHash != previousBlock.Hash)
                return false;
        }

        return true;
    }
}