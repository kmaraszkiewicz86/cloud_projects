using System.ComponentModel.DataAnnotations;

namespace PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery
{
    public class InsertPhotoGralleryRequest
    {
        [Required]
        public string Name { get; set; }

        public string PhotoInBytes { get; set; }
    }
}