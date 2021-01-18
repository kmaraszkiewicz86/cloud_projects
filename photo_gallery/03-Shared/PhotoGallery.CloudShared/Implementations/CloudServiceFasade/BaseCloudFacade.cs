using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoGallery.CloudShared.Interfaces;
using PhotoGallery.Shared.ApiModels.Api.PhotoGallery;
using PhotoGallery.Shared.Extensions;
using PhotoGallery.Shared.Models;

namespace PhotoGallery.CloudShared.Implementations.CloudServiceFasade
{
    public abstract class BaseCloudFacade : IBaseCloudFacade
    {
        protected readonly INoSqlDbCloudServiceFactory NoSqlDbCloudServiceFactory;
        
        private readonly INoSqlDbCloudService _noSqlDbCloudService;

        public BaseCloudFacade(INoSqlDbCloudServiceFactory noSqlDbCloudServiceFactory)
        {
            NoSqlDbCloudServiceFactory = noSqlDbCloudServiceFactory;
            _noSqlDbCloudService = GetNoSqlDbCloudService();
        }

        public async Task InsertPhotoAsync(InsertPhotoGralleryRequest insertPhotoGralleryRequest)
        {
            await _noSqlDbCloudService.InsertAsync(insertPhotoGralleryRequest.ToPhotoGalleryModel());
        }
        
        public async Task<IEnumerable<PhotoGalleryModel>> GetAllAsync()
        {
            return await _noSqlDbCloudService.GetAllAsync();
        }
        
        protected abstract INoSqlDbCloudService GetNoSqlDbCloudService();
    }
}