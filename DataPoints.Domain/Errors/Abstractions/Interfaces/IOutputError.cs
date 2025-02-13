namespace DataPoints.Domain.Errors.Abstractions.Interfaces;

public interface IOutputError
{
    public string? Code { get; init; }
    public string? Description { get; init; }
    public string? Date { get; set; }
    public int? StatusCode { get; set; }
}