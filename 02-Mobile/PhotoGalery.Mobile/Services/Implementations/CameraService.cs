using System;
using System.IO;
using System.Threading.Tasks;
using PhotoGalery.Mobile.Services.Interfaces;
using PhotoGallery.Shared.Models;
using Xamarin.Essentials;

namespace PhotoGalery.Mobile.Services.Implementations
{
    public class CameraService : ICameraService
    {
        public async Task<PhotoFromCameraModel> TakePhotoAndGetBytes()
        {
            var photoFromCameraModelResult = new PhotoFromCameraModel();

            try
            {
                FileResult fileResult = await MediaPicker.CapturePhotoAsync();

                if (IsUserCanceledOperation(fileResult))
                {
                    return photoFromCameraModelResult;
                }

                photoFromCameraModelResult.PhotoInBytes = await GetBytesFromPhotoAsync(fileResult);
            }
            catch (Exception exception)
            {
                photoFromCameraModelResult.ErrorMessage = exception.Message;
            }

            return photoFromCameraModelResult;
        }

        private bool IsUserCanceledOperation(FileResult fileResult)
        {
            return fileResult == null;
        }

        private async Task<Stream> GetBytesFromPhotoAsync(FileResult fileResult)
        {
            Stream newStream = null;


            var newFile = Path.Combine(FileSystem.CacheDirectory, fileResult.FileName);
            using (var stream = await fileResult.OpenReadAsync())
            using (newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            return File.OpenRead(newFile);
        }
    }
}