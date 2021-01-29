using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;
using PhotoGallery.Shared.Models;

namespace PhotoGallery.CloudShared.Interfaces
{
    public interface IBaseCloudFacade
    {
        Task InsertAsync(InsertPhotoGralleryRequest insertPhotoGralleryRequest);

        Task DeleteAsync(string id);
        
        Task<IEnumerable<PhotoGalleryResponse>> GetAllAsync();

        Task<PhotoGalleryResponse> FindAsync(string id);
    }
}