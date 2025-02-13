using DataPoints.Domain.Enums;

namespace DataPoints.Domain.Database.Entity.Interfaces;

public interface IAudit
{
    public string Operation { get; }
    public InternalOperation InternalOperation { get; set; }
    public string LastChangeBy { get; set; } 
    public DateTime LastChangeAt { get; set; }
}
