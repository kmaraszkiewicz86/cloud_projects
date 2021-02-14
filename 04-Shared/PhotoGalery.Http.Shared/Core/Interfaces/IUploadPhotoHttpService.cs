using System.IO;
using System.Threading.Tasks;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;

namespace PhotoGalery.Http.Shared.Core.Interfaces
{
    public interface IUploadPhotoHttpService
    {
        Task<UploadedPhotoResponse> UploadAsync(string id, Stream photoStream);
    }
}