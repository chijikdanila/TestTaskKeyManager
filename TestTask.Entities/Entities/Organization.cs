namespace TestTask.Entities.Entities;

public class Organization
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<Key> KeysInfo { get; set; }

    public static Organization Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Organization name cannot be empty");
        }
            
        return new Organization
        {
            Name = name
        };
    }

    public void Update(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Organization name cannot be empty");
        }

        Name = name;
    }
}