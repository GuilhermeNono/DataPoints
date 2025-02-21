using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity;
using DataPoints.Domain.Enums;

namespace DataPoints.Domain.Entities.Main;

[Table("Ppl_People")]
public class PersonEntity : AuditableStatefulEntity<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Avatar { get; set; } 
    public DateTime BirthDate { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public string NormalizedDocumentNumber { get; set; } = string.Empty;
    public DocumentType DocumentType { get; set; }
    public PersonType PersonType { get; set; }
    public DateTime DateInclusion { get; set; } = DateTime.Now;
    public string UserName => $"{FirstName} {LastName}#{Id}";
}
