using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.CloudShared.Interfaces;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;

namespace PhotoGalery.Api.Controllers.Api.AwsPhotoGallery
{
    
    public class FindOneAwsPhotoGalleryControllerController : BaseAwsPhotoGalleryController
    {
        public FindOneAwsPhotoGalleryControllerController(IAwsCloudFacade awsCloudFacade) : base(awsCloudFacade)
        {
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoGalleryResponse>> FindOneAsync(
            string id)
        {
            ValidateIdParam(id);
            
            return Ok(await _awsCloudFacade.FindAsync(id));
        }
    }
}