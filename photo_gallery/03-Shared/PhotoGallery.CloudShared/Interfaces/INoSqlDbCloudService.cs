using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoGallery.Shared.Models;

namespace PhotoGallery.CloudShared.Interfaces
{
    public interface INoSqlDbCloudService
    {
        Task InsertAsync(PhotoGalleryModel photoGalleryModel);

        Task DeleteAsync(string id);

        Task<IEnumerable<PhotoGalleryModel>> GetAllAsync();

        Task<PhotoGalleryModel> FindAsync(string id);
    }
}