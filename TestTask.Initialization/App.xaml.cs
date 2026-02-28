using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestTask.BLL.Services;
using TestTask.Persistence.Postgres;
using TestTask.Persistence.Postgres.Repositories;
using TestTask.Persistence.Postgres.Seeders;
using TestTask.PL.ViewModels.UserControls;
using TestTask.PL.ViewModels.Windows;
using TestTask.PL.Views.Windows;
using TestTask.Services.Services;
using TestTask.Utilities.DependencyServices;

namespace TestTask.Initialization;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IHost _host;
    
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var builder = Host.CreateApplicationBuilder();
        builder.Configuration
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        
        if (e.Args is not null && 
            e.Args.Length > 0 && 
            e.Args[0] == "migrate")
        {
            builder.Services.AddSingleton<IDbSeeder, OrganizationDbSeeder>();
            builder.Services.AddSingleton<IDbSeeder, KeyDbSeeder>();
            
            _host = builder.Build();
            
            var dbContext = _host.Services.GetRequiredService<AppDbContext>();
            
            await dbContext.Database.EnsureCreatedAsync();
        
            await dbContext.Database.MigrateAsync();
            
            var seeders = _host.Services
                .GetServices<IDbSeeder>()
                .OrderBy(o => o.Order);

            var groupSeeders = seeders.GroupBy(x => x.Order);

            if (groupSeeders.Any(x => x.Count() > 1))
            {
                throw new Exception("Incorrect order of seeders");
            }
            
            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync();
            }
            
            Current.Shutdown();
            
            return;
        }

        builder.Services.AddSingleton<IOrganizationsDataReportService, DocxGenerator>();
        
        builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        builder.Services.AddScoped<IKeyRepository, KeyRepository>();

        builder.Services.AddScoped<IOrganizationsService, OrganizationsDataAccessService>();
        builder.Services.AddScoped<IKeysService, KeysesDataAccessService>();
        
        builder.Services.AddSingleton<MainWindowVM>();
        builder.Services.AddSingleton<OrganizationsTabVM>();
        builder.Services.AddSingleton<KeysTabVM>();
        
        _host = builder.Build();
        DependencyResolver.Initialize(_host.Services);
        
        var vm = _host.Services.GetRequiredService<MainWindowVM>();
        var mainWindow = new MainWindow(vm);
        
        MainWindow = mainWindow;
        
        MainWindow.Show();
        
        base.OnStartup(e);
    }
}