using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoGallery.CloudShared.Interfaces;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;
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

        public async Task InsertAsync(InsertPhotoGralleryRequest insertPhotoGralleryRequest)
        {
            await _noSqlDbCloudService.InsertAsync(insertPhotoGralleryRequest.ToPhotoGalleryModel());
        }
        
        public async Task DeleteAsync(string id)
        {
            await _noSqlDbCloudService.DeleteAsync(id);
        }
        
        public async Task<IEnumerable<PhotoGalleryResponse>> GetAllAsync()
        {
            return (await _noSqlDbCloudService.GetAllAsync()).ToPhotoGalleryResponses();
        }
        
        public async Task<PhotoGalleryResponse> FindAsync(string id)
        {
            return (await _noSqlDbCloudService.FindAsync(id)).ToPhotoGalleryResponse();
        }
        
        protected abstract INoSqlDbCloudService GetNoSqlDbCloudService();
    }
}