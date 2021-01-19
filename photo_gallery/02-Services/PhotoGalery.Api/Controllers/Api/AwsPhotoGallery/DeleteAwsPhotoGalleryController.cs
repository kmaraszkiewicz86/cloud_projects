using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.CloudShared.Interfaces;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;

namespace PhotoGalery.Api.Controllers.Api.AwsPhotoGallery
{
    public class DeleteAwsPhotoGalleryController : BaseAwsPhotoGalleryController
    {
        public DeleteAwsPhotoGalleryController(IAwsCloudFacade awsCloudFacade) : base(awsCloudFacade)
        {
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(DeleteRequest deleteRequest)
        {
            await _awsCloudFacade.DeleteAsync(deleteRequest.Id);
            return Ok();
        }
    }
}