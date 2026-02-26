using Primitives.Enums;

namespace Entities;

public class Key
{
    public long Id { get; set; }
    public Guid KeyValue { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public long OrganizationId { get; set; }
    public Organization Organization { get; set; } = null!;
    public KeyBlockStatus KeyBlockStatus { get; set; }

    public static Key Create(Guid keyValue, DateTime startDate, DateTime endDate, long organizationId)
    {
        if (keyValue == Guid.Empty)
        {
            throw new ArgumentException("Key value cannot be empty");
        }
        
        if (startDate > endDate)
        {
            throw new ArgumentException("Start date must be <= end date");
        }

        return new Key()
        {
            KeyValue = keyValue,
            StartedAt = startDate,
            EndedAt = endDate,
            OrganizationId = organizationId,
            KeyBlockStatus = KeyBlockStatus.Active
        };
    }

    public void Update(Guid keyValue, DateTime startDate, DateTime endDate, long organizationId)
    {
        if (keyValue == Guid.Empty)
        {
            throw new ArgumentException("Key value cannot be empty");
        }
        
        if (startDate > endDate)
        {
            throw new ArgumentException("Start date must be <= end date");
        }

        KeyValue = keyValue;
        StartedAt = startDate;
        EndedAt = endDate;
        OrganizationId = organizationId;
    }
    
    public void SetBlockStatus(bool blockStatus)
    {
        KeyBlockStatus = blockStatus ? KeyBlockStatus.Blocked : KeyBlockStatus.Active;
    }
}