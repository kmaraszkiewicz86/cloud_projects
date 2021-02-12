using System.IO;

namespace PhotoGallery.Shared.Models
{
    public class PhotoFromCameraModel : BaseValidationModel
    {
        public Stream PhotoInBytes { get; set; }

        public bool WasCanceled => PhotoInBytes == null || PhotoInBytes.Length == 0;
    }
}