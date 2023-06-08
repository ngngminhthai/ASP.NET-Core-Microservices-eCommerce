using Contracts.Domains.Interfaces;

namespace Contracts.Common.Events;

public abstract class AuditableEventEntity<T> : EventEntity<T>, IAuditable
{
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? LastModifiedDate { get; set; }
}