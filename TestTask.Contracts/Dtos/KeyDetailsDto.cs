using TestTask.Primitives.Enums;

namespace TestTask.Contracts.Dtos;

public class KeyDetailsDto
{
    public long Id { get; set; }
    public Guid KeyValue { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public long OrganizationId { get; set; }
    public string OrganizationName { get; set; }
    public KeyStatus KeyStatus { get; set; }

    public KeyDetailsDto(
        long id,
        Guid keyValue,
        DateTime startedAt,
        DateTime endedAt,
        long organizationId,
        string organizationName,
        KeyStatus keyStatus)
    {
        Id = id;
        KeyValue = keyValue;
        StartedAt = startedAt;
        EndedAt = endedAt;
        OrganizationId = organizationId;
        OrganizationName = organizationName;
        KeyStatus = keyStatus;
    }
}