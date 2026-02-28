using TestTask.Primitives.Enums;

namespace TestTask.Entities.Entities;

public class Key
{
    public long Id { get; set; }
    public Guid KeyValue { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public long OrganizationId { get; set; }
    public Organization Organization { get; set; } = null!;
    public KeyStatus KeyStatus { get; set; }

    public static Key Create(Guid keyValue, DateTime startDate, DateTime endDate, long organizationId,  KeyStatus keyStatus)
    {
        if (keyValue == Guid.Empty)
        {
            throw new ArgumentException("Ключ не может быть пустым");
        }

        if (startDate > endDate)
        {
            throw new ArgumentException("Дата активации должна быть меньше либо равно дате окончания");
        }

        return new Key()
        {
            KeyValue = keyValue,
            StartedAt = ConvertToUtc(startDate),
            EndedAt = ConvertToUtc(endDate),
            OrganizationId = organizationId,
            KeyStatus = keyStatus
        };
    }

    public void Update(Guid keyValue, DateTime startDate, DateTime endDate, long organizationId, KeyStatus keyStatus)
    {
        if (keyValue == Guid.Empty)
        {
            throw new ArgumentException("Ключ не может быть пустым");
        }

        if (startDate > endDate)
        {
            throw new ArgumentException("Дата активации должна быть меньше либо равно дате окончания");
        }

        KeyValue = keyValue;
        StartedAt = ConvertToUtc(startDate);
        EndedAt = ConvertToUtc(endDate);
        OrganizationId = organizationId;
        KeyStatus = keyStatus;
    }

    public void SetBlockStatus(KeyStatus status)
    {
        KeyStatus = status;
    }

    private static DateTime ConvertToUtc(DateTime dt)
    {
        return dt.Kind switch
        {
            DateTimeKind.Utc => dt,
            DateTimeKind.Local => dt.ToUniversalTime(),
            _ => DateTime.SpecifyKind(dt, DateTimeKind.Utc)
        };
    }
}