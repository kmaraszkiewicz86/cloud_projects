using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PhotoGalery.Http.Shared.Core.Interfaces;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;

namespace PhotoGalery.Components
{
    public class AddPhotoGaleryComponent : ComponentShared
    {
        [Parameter]
        public EventCallback OnAddedCallback { get; set; }

        [Inject]
        public IPhotoGaleryHttpService PhotoGaleryHttpService { get; set; }

        public InsertPhotoGralleryRequest InsertPhotoGralleryRequest = new InsertPhotoGralleryRequest();

        public async Task OnInvalidSubmitAsync()
        {
            await OnTryCatchAsync(async () =>
            {
                await PhotoGaleryHttpService.InsertAsync(InsertPhotoGralleryRequest);
            });

            InsertPhotoGralleryRequest = new InsertPhotoGralleryRequest();
            await OnAddedCallback.InvokeAsync();
        }
    }
}