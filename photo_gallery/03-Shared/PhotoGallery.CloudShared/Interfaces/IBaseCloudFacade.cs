using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoGallery.Shared.ApiModels.Api.PhotoGallery;
using PhotoGallery.Shared.Models;

namespace PhotoGallery.CloudShared.Interfaces
{
    public interface IBaseCloudFacade
    {
        Task InsertPhotoAsync(InsertPhotoGralleryRequest insertPhotoGralleryRequest);

        Task<IEnumerable<PhotoGalleryModel>> GetAllAsync();
    }
}