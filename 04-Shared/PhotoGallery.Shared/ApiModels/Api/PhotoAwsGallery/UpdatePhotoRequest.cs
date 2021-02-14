using System.ComponentModel.DataAnnotations;
using System.IO;

namespace PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery
{
    public class UpdatePhotoRequest
    {
        [Required]
        public Stream PhotoStream { get; set; }

        public UpdatePhotoRequest()
        {

        }

        public UpdatePhotoRequest(Stream photoStream)
        {
            PhotoStream = photoStream;
        }
    }
}