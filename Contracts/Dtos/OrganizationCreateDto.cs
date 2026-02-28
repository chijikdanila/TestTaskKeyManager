namespace Contracts.Dtos;

public class OrganizationCreateDto
{
    public string Name { get; set; }

    public OrganizationCreateDto(string name)
    {
        Name = name;
    }
}