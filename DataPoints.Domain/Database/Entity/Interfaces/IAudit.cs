using DataPoints.Domain.Enums;

namespace DataPoints.Domain.Database.Entity.Interfaces;

public interface IAudit
{
    public string Operation { get; }
    public OperationEnum OperationEnum { get; set; }
    public string AuditUser { get; set; } 
    public DateTime AuditDate { get; set; }
}
