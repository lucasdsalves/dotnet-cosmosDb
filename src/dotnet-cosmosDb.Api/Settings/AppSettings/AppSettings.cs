using dotnet_cosmosDb.Api.Domain.Product.Repository;
using dotnet_cosmosDb.Api.Infra.CosmosDb;
using dotnet_cosmosDb.Api.Infra.CosmosDb.Repositories.Product;
using dotnet_cosmosDb.Api.Settings.CosmosDb;

namespace dotnet_cosmosDb.Api.Settings.AppSettings
{
    public static class AppSettings
    {
        public static WebApplicationBuilder AddAppSettingsConfiguration(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Services.Configure<CosmosDbSettings>(
                builder.Configuration.GetSection("CosmosDb")
            );

            builder.Services.AddSingleton<CosmosDbSettings>();
            builder.Services.AddSingleton<ICosmosDbFactory, CosmosDbFactory>();
            builder.Services.AddTransient<Dependencies, Dependencies>();
            builder.Services.AddTransient<CosmosDbStartup, CosmosDbStartup>();

            ServiceProvider s = builder.Services.BuildServiceProvider();
            IServiceScope scope = s.CreateScope();
            var dbStartup = scope.ServiceProvider.GetService<CosmosDbStartup>();
            dbStartup.StartupCosmosDb().GetAwaiter().GetResult();

            builder.Services.AddSingleton<IProductRepository, ProductRepository>();



            return builder;
        }
    }
}
