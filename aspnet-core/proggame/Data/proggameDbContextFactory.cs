using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace proggame.Data;

public class proggameDbContextFactory : IDesignTimeDbContextFactory<proggameDbContext>
{
    public proggameDbContext CreateDbContext(string[] args)
    {

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<proggameDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new proggameDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
