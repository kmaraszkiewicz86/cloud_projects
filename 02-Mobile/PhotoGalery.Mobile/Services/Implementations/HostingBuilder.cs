using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhotoGalery.Http.Shared.Extensions;
using PhotoGalery.Mobile.Extensions;
using Xamarin.Essentials;

namespace PhotoGalery.Mobile.Services.Implementations
{
    public class HostingBuilder
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        private IHostBuilder _hostingBuilder;

        public HostingBuilder InitializeRequiredServices()
        {
            _hostingBuilder = new HostBuilder()
                .ConfigureHostConfiguration(ConfigureHostConfiguration)
                .ConfigureServices(ConfigureServices);

            return this;
        }

        public void Build()
        {
            ServiceProvider = _hostingBuilder.Build().Services;
        }

        private void ConfigureHostConfiguration(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.AddCommandLine(
                new [] {$"ContentRoot={FileSystem.AppDataDirectory}"});
        }

        private void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.GetRequiredHttpServices()
                .GetRequiredServices()
                .GetRequiredViewModels()
                .GetRequiredPages();
        }
    }
}