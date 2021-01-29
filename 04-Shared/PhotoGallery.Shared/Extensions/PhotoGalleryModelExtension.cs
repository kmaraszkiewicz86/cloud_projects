using System.Collections.Generic;
using System.Linq;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;
using PhotoGallery.Shared.Models;

namespace PhotoGallery.Shared.Extensions
{
    public static class PhotoGalleryModelExtension
    {
        public static PhotoGalleryResponse ToPhotoGalleryResponse(this PhotoGalleryModel photoGalleryModel)
        {
            var photoGalleryResponse = new PhotoGalleryResponse();

            if (photoGalleryModel == null)
                return photoGalleryResponse;
            
            photoGalleryResponse.Id = photoGalleryModel.Id;
            photoGalleryResponse.Name = photoGalleryModel.Name;

            return photoGalleryResponse;
        }

        public static IEnumerable<PhotoGalleryResponse> ToPhotoGalleryResponses(
            this IEnumerable<PhotoGalleryModel> photoGalleryModels)
        {
            return photoGalleryModels == null 
                ? new List<PhotoGalleryResponse>() 
                : photoGalleryModels.Select(p => p.ToPhotoGalleryResponse());
        }
    }
}