using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity;

namespace DataPoints.Domain.Entities.Main;

[Table("Wlt_Transactions")]
public class WalletTransactionEntity : AuditableEntity<Guid>
{
    public Guid IdWalletFrom { get; set; }
    public Guid IdWalletTo { get; set; }
    public decimal Amount { get; set; }
    public bool IsCredit { get; set; }
    public string TransactionHash { get; set; } = string.Empty;
}
