using todoappBE.Core.Interfaces;
using todoappBE.Core.Services;
using todoappBE.Infrastructure.Data;
using todoappBE.Infrastructure.Data.Queries;
using todoappBE.UseCases.Contributors.List;


namespace todoappBE.Infrastructure;
public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger)
  {
    string? connectionString = config.GetConnectionString("PostgreSqlConnection");
    Guard.Against.Null(connectionString);
    services.AddDbContext<AppDbContext>(options =>
     options.UseNpgsql(connectionString));

    services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
           .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>))
           .AddScoped<IListContributorsQueryService, ListContributorsQueryService>()
           .AddScoped<IDeleteContributorService, DeleteContributorService>();


    logger.LogInformation("{Project} services registered", "Infrastructure");

    return services;
  }
}
