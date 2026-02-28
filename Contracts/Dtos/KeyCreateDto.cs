using Primitives.Enums;

namespace Contracts.Dtos;

public class KeyCreateDto
{
    public Guid KeyValue { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public long OrganizationId { get; set; }
    public KeyStatus KeyStatus { get; set; }

    public KeyCreateDto(
        Guid keyValue,
        DateTime startedAt,
        DateTime endedAt,
        long organizationId,
        KeyStatus keyStatus)
    {
        KeyValue = keyValue;
        StartedAt = startedAt;
        EndedAt = endedAt;
        OrganizationId = organizationId;
        KeyStatus = keyStatus;
    }
}