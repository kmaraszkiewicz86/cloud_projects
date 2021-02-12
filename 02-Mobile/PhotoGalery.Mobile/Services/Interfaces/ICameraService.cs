using System;
using System.Threading.Tasks;
using PhotoGallery.Shared.Models;

namespace PhotoGalery.Mobile.Services.Interfaces
{
    public interface ICameraService
    {
        Task<PhotoFromCameraModel> TakePhotoAndGetBytes();
    }
}