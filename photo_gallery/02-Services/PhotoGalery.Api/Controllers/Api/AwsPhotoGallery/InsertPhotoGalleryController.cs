using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.CloudShared.Interfaces;
using PhotoGallery.Shared.ApiModels.Api.PhotoGallery;

namespace PhotoGalery.Api.AwsPhotoGallery
{
    [ApiController]
    [Route("api/AwsPhotoGallery")]
    public class InsertPhotoGalleryController : ControllerBase
    {
        private readonly IAwsCloudFacade _awsCloudFacade;

        public InsertPhotoGalleryController(IAwsCloudFacade awsCloudFacade)
        {
            _awsCloudFacade = awsCloudFacade;
        }

        [HttpPost]
        public async Task<IActionResult> InsertPhotoGalleryAsync(
            [FromBody] InsertPhotoGralleryRequest insertPhotoGralleryRequest)
        {
            await _awsCloudFacade.InsertPhotoAsync(insertPhotoGralleryRequest);
            return Ok();
        }
    }
}