using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoGallery.Shared.Models;

namespace PhotoGallery.CloudShared.Interfaces
{
    public interface IAwsDynamoDbCloudService
    {
        Task InsertAsync(PhotoGalleryModel photoGalleryModel);

        Task DeleteAsync(string id);

        Task<IEnumerable<PhotoGalleryModel>> GetAllAsync();

        Task<PhotoGalleryModel> FindAsync(string id);
    }
}