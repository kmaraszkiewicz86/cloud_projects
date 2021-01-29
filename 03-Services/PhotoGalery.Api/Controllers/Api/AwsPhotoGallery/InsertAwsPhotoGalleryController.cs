using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.CloudShared.Interfaces;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;

namespace PhotoGalery.Api.Controllers.Api.AwsPhotoGallery
{
    public class InsertPhotoGalleryController : BaseAwsPhotoGalleryController
    {
        public InsertPhotoGalleryController(IAwsCloudFacade awsCloudFacade) : base(awsCloudFacade)
        {
        }
        
        [HttpPost]
        public async Task<IActionResult> InsertPhotoGalleryAsync(
            [FromBody] InsertPhotoGralleryRequest insertPhotoGralleryRequest)
        {
            await _awsCloudFacade.InsertAsync(insertPhotoGralleryRequest);
            return Ok();
        }
    }
}