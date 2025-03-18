using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using DataPoints.Domain.Errors.Abstractions.Interfaces;

namespace DataPoints.Domain.Errors.Abstractions;

public abstract class Error : IOutputError
{
    [JsonIgnore] public string? Code { get; init; } = nameof(HttpStatusCode.InternalServerError);

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; init; } = "Houve um problema interno, por favor tente mais tarde.";

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Date { get; set; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

    [JsonIgnore] public int? StatusCode { get; set; } = (int)HttpStatusCode.InternalServerError;

    public override string ToString() => JsonSerializer.Serialize(this);
}