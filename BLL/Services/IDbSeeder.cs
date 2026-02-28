namespace BLL.Services;

public interface IDbSeeder
{
    int Order { get; }
    Task SeedAsync();
}