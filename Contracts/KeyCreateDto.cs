using Primitives.Enums;

namespace Contracts;

public class KeyCreateDto
{
    public Guid KeyId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public long OrganizationId { get; set; }
    public KeyBlockStatus KeyBlockStatus { get; set; }
    public bool IsBlocked { get; set; } //только для UI
}