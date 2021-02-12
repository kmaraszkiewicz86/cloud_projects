namespace PhotoGallery.Shared.Models
{
    public abstract class BaseValidationModel
    {
        public string ErrorMessage { get; set; }

        public bool IsValid => string.IsNullOrWhiteSpace(ErrorMessage);
    }
}