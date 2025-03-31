namespace DataPoints.Contract.Block.Validation;

public record BlockValidationResponse(bool IsValid)
{
    public static BlockValidationResponse Invalid() => new BlockValidationResponse(false);
    public static BlockValidationResponse Valid() => new BlockValidationResponse(true);
}