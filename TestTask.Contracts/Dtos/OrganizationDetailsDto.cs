namespace TestTask.Contracts.Dtos;

public class OrganizationDetailsDto
{
    public long Id { get; set; }
    public string Name { get; set; }

    public OrganizationDetailsDto(long id, string name)
    {
        Id = id;
        Name = name;
    }
}