using Microsoft.Extensions.DependencyInjection;

namespace PhotoGalery.Mobile.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection GetRequiredPages(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<MainPage>();

            return serviceCollection;
        }
    }
}