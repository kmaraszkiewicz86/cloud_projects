using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.CloudShared.Interfaces;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoGalery.Api.Controllers.Api.AwsPhotoGallery
{
    public class UploadPhotoController : BaseAwsPhotoGalleryController
    {
        public UploadPhotoController(IAwsCloudFacade awsCloudFacade) : base(awsCloudFacade)
        {
        }

        [HttpPut("UploadPhoto/{id}")]
        public async Task<ActionResult<UploadedPhotoResponse>> UploadPhotoAsync(string id)
        {
            var uploadedPhotoResponse = new UploadedPhotoResponse();
            var photoStream = Request.Body;

            using (var memoryStream = new MemoryStream())
            {
                await photoStream.CopyToAsync(memoryStream);

                uploadedPhotoResponse.PhotoBytes = memoryStream.ToArray();
            }

            return Ok(uploadedPhotoResponse);
        }
    }
}