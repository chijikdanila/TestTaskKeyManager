using Primitives.Enums;

namespace Contracts;

public class KeyDetailsDto
{
    public long Id { get; set; }
    public Guid KeyValue { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public long OrganizationId { get; set; }
    public string OrganizationName { get; set; } = string.Empty;
    public KeyBlockStatus KeyBlockStatus { get; set; }
    public bool IsBlocked => KeyBlockStatus == KeyBlockStatus.Blocked; //только для UI
}