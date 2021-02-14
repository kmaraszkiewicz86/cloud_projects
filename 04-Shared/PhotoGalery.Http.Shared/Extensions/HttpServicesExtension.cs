using Microsoft.Extensions.DependencyInjection;
using PhotoGalery.Http.Shared.Core.Implementations;
using PhotoGalery.Http.Shared.Core.Interfaces;

namespace PhotoGalery.Http.Shared.Extensions
{
    public static class HttpServicesExtension
    {
        public static IServiceCollection GetRequiredHttpServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient<IPhotoGaleryHttpService, PhotoGaleryHttpService>();
            serviceCollection.AddHttpClient<IUploadPhotoHttpService, UploadPhotoHttpService>();

            return serviceCollection;
        }
    }
}