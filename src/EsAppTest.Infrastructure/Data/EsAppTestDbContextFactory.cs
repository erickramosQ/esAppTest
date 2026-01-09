using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EsAppTest.Infrastructure.Data;

public sealed class EsAppTestDbContextFactory : IDesignTimeDbContextFactory<EsAppTestDbContext>
{
    public EsAppTestDbContext CreateDbContext(string[] args)
    {
        var repoRoot = Directory.GetCurrentDirectory();

        if (repoRoot.EndsWith(Path.Combine("src", "EsAppTest.Api")))
            repoRoot = Path.GetFullPath(Path.Combine(repoRoot, "..", ".."));

        var apiPath = Path.Combine(repoRoot, "src", "EsAppTest.Api");
        var appsettings = Path.Combine(apiPath, "appsettings.json");

        if (!File.Exists(appsettings))
            throw new FileNotFoundException($"No se encontró appsettings.json en: {appsettings}");

        var config = new ConfigurationBuilder()
            .SetBasePath(apiPath)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var cs = config.GetConnectionString("SqlServer")
                 ?? throw new InvalidOperationException("No se encontró ConnectionStrings:SqlServer");

        var options = new DbContextOptionsBuilder<EsAppTestDbContext>()
            .UseSqlServer(cs)
            .Options;

        return new EsAppTestDbContext(options);
    }
}
