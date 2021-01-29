using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.CloudShared.Interfaces;
using PhotoGallery.Shared.Models;

namespace PhotoGalery.Api.Controllers.Api.AwsPhotoGallery
{
    [ApiController]
    [Route("api/AwsPhotoGallery")]
    public class GetAllPhotoGalleryController : BaseAwsPhotoGalleryController
    {
        public GetAllPhotoGalleryController(IAwsCloudFacade awsCloudFacade) : base(awsCloudFacade)
        {
        }

        [HttpGet]
        public async Task<ActionResult<PhotoGalleryModel>> InsertPhotoGalleryAsync()
        {
            return Ok(await _awsCloudFacade.GetAllAsync());
        }
    }
}