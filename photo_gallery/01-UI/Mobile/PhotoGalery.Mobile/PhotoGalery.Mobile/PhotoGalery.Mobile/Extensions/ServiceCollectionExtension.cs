using Microsoft.Extensions.DependencyInjection;
using PhotoGalery.Mobile.ViewModels;

namespace PhotoGalery.Mobile.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection GetRequiredPages(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<MainPage>();

            return serviceCollection;
        }

        public static IServiceCollection GetRequiredViewModels(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<MainViewModel>();

            return serviceCollection;
        }
    }
}