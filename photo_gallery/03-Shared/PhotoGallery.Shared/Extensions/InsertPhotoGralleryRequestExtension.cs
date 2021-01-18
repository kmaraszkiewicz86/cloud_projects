using System;
using PhotoGallery.Shared.ApiModels.Api.PhotoGallery;
using PhotoGallery.Shared.Models;

namespace PhotoGallery.Shared.Extensions
{
    public static class InsertPhotoGralleryRequestExtension
    {
        public static PhotoGalleryModel ToPhotoGalleryModel(
            this InsertPhotoGralleryRequest insertPhotoGralleryRequest)
        {
            if (insertPhotoGralleryRequest == null)
                return null;

            return new PhotoGalleryModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = insertPhotoGralleryRequest.Name,
                BucketName = "BucketItemTest",
                KeyName = "KeyName1"
            };
        }
    }
}