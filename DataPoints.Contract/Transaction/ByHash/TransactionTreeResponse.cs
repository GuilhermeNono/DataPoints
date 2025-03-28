using System.Text.Json.Serialization;
using DataPoints.Domain.Enums;

namespace DataPoints.Contract.Transaction.ByHash;

public class TransactionTreeResponse
{
    public TransactionDelivererResponse From { get; set; } = new(); 
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TransactionDelivererResponse? To { get; set; } = new();
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
}