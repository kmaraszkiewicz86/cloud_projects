using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.CloudShared.Interfaces;
using PhotoGallery.Shared.ApiModels.Api.PhotoGallery;
using PhotoGallery.Shared.Models;

namespace PhotoGalery.Api.AwsPhotoGallery
{
    [ApiController]
    [Route("api/AwsPhotoGallery")]
    public class GetAllPhotoGalleryController : ControllerBase
    {
        private readonly IAwsCloudFacade _awsCloudFacade;

        public GetAllPhotoGalleryController(IAwsCloudFacade awsCloudFacade)
        {
            _awsCloudFacade = awsCloudFacade;
        }

        [HttpGet]
        public async Task<ActionResult<PhotoGalleryModel>> InsertPhotoGalleryAsync()
        {
            return Ok(await _awsCloudFacade.GetAllAsync());
        }
    }
}