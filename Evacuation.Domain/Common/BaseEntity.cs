using System.ComponentModel.DataAnnotations;

namespace Evacuation.Domain.Common;

public abstract class BaseEntity
{
    [Key]
    public int Id { get;  set; }
    public DateTime CreatedAt { get;  set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get;  set; }

    public void SetUpdated() => UpdatedAt = DateTime.UtcNow;
    public void SetCreated() => CreatedAt = DateTime.UtcNow;
}