using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;

namespace PhotoGalery.Http.Shared.Core.Interfaces
{
    public interface IPhotoGaleryHttpService
    {
        Task<IEnumerable<PhotoGalleryResponse>> GetAllAsync();

        Task InsertAsync(InsertPhotoGralleryRequest insertPhotoGralleryRequest);

        Task DeleteAsync(DeleteRequest deleteRequest);
    }
}