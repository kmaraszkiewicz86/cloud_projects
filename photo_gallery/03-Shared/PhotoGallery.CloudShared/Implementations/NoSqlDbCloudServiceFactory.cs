using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using PhotoGallery.CloudShared.Interfaces;

namespace PhotoGallery.CloudShared.Implementations
{
    public class NoSqlDbCloudServiceFactory : INoSqlDbCloudServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public NoSqlDbCloudServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public INoSqlDbCloudService GetAwsDynamoNoSqlDbCloudService()
        {
            return GetServices().FirstOrDefault(s 
                => s.GetType() == typeof(AwsDynamoNoSqlDbCloudService));
        }

        private IEnumerable<INoSqlDbCloudService> GetServices()
        {
            return _serviceProvider.GetServices<INoSqlDbCloudService>();
        }
    }
}