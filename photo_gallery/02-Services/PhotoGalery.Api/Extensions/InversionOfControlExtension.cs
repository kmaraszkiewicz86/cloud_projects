using Microsoft.Extensions.DependencyInjection;
using PhotoGallery.CloudShared.Implementations;
using PhotoGallery.CloudShared.Implementations.CloudServiceFasade;
using PhotoGallery.CloudShared.Interfaces;

namespace PhotoGalery.Api.Extensions
{
    public static class InversionOfControlExtension
    {
        public static IServiceCollection ConfigureRequiredServices(this IServiceCollection services)
        {
            services.AddScoped<INoSqlDbCloudService, AwsDynamoNoSqlDbCloudService>();
            services.AddScoped<INoSqlDbCloudServiceFactory, NoSqlDbCloudServiceFactory>();
            services.AddScoped<IAwsCloudFacade, AwsCloudFacade>();
            
            return services;
        }
    }
}