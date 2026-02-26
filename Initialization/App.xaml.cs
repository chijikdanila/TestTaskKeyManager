using System.Configuration;
using System.Data;
using System.Windows;
using BLL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Postgres;
using Persistence.Postgres.Repositories;
using Services;

namespace Initialization;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        services.AddSingleton<IConfiguration>(config);
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IKeyRepository, KeyRepository>();

        services.AddScoped<IOrganizationService, OrganizationDataAccessService>();
        //добавить KeyDataAccessService, Views и ViewModels
    }
}