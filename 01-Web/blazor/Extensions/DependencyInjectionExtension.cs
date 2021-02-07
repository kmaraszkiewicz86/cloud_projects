using Microsoft.Extensions.DependencyInjection;
using PhotoGalery.Http.Shared.Extensions;

namespace PhotoGalery.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            return services.GetRequiredHttpServices();
        }
    }
}