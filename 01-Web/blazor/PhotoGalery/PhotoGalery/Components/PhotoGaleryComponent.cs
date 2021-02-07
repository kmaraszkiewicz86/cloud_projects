using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PhotoGalery.Http.Shared.Core.Interfaces;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;
using PhotoGallery.Shared.Exceptions;

namespace PhotoGalery.Components
{
    public class PhotoGaleryComponent : ComponentShared
    {
        [Inject]
        public IPhotoGaleryHttpService PhotoGaleryHttpService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected List<PhotoGalleryResponse> PhotoGalleryResponses { get; private set; }

        public PhotoGaleryComponent()
        {
            PhotoGalleryResponses = new List<PhotoGalleryResponse>();
        }

        public void NavigateToPhotoDetailsPage(string id)
        {
            NavigationManager.NavigateTo($"PhotoGaleryDetailsPage/{id}");
        }

        public async Task OnAddedItemHandler()
        {
            await LoadItemAsync();
        }

        public async Task DeletePhotoAsync(string id)
        {
            await OnTryCatchAsync(async () =>
            {
                await PhotoGaleryHttpService.DeleteAsync(new DeleteRequest { Id = id });
            });

            await LoadItemAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            await OnTryCatchAsync(async () =>
            {
                await LoadItemAsync();
            });
        }

        private async Task LoadItemAsync()
        {
            await OnTryCatchAsync(async () =>
            {
                PhotoGalleryResponses.Clear();
                PhotoGalleryResponses.AddRange(await PhotoGaleryHttpService.GetAllAsync());
            });
        }
    }
}