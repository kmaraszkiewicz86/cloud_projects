using Microsoft.AspNetCore.Mvc;
using PhotoGallery.CloudShared.Interfaces;
using PhotoGallery.Shared.Exceptions;

namespace PhotoGalery.Api.Controllers.Api.AwsPhotoGallery
{
    [ApiController]
    [Route("api/AwsPhotoGallery")]
    public abstract class BaseAwsPhotoGalleryController : ControllerBase
    {
        protected readonly IAwsCloudFacade _awsCloudFacade;

        protected BaseAwsPhotoGalleryController(IAwsCloudFacade awsCloudFacade)
        {
            _awsCloudFacade = awsCloudFacade;
        }

        protected void ValidateIdParam(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new BadRequestException($"The id can't be empty");
            }
        }
    }
}