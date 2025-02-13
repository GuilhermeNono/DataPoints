using System.Text.Json;
using System.Text.Json.Serialization;
using DataPoints.Domain.Errors.Abstractions.Interfaces;

namespace DataPoints.Domain.Errors.Abstractions;

public abstract class Error : IOutputError
{
    [JsonIgnore]
    public string? Code { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Date { get; set; }
    [JsonIgnore]
    public int? StatusCode { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}