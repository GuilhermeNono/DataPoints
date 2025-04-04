﻿using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity;

namespace DataPoints.Domain.Entities.Main;

[Table("Wlt_Wallets")]
public class WalletEntity : AuditableStatefulEntity<Guid>
{
    public Guid IdUser { get; set; }
    public string PublicKey { get; set; } = string.Empty;
    public string Hash { get; set; } = string.Empty;
    public bool IsBlocked { get; set; }
}
