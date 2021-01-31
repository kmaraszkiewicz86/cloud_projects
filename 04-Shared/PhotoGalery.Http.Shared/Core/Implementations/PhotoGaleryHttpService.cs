using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PhotoGalery.Http.Shared.Core.Interfaces;
using PhotoGalery.Http.Shared.Extensions;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;

namespace PhotoGalery.Http.Shared.Core.Implementations
{
    public class PhotoGaleryHttpService : BaseHttpService, IPhotoGaleryHttpService
    {
        public PhotoGaleryHttpService(HttpClient httpClient) : base(httpClient)
        {
            
        }

        public async Task<IEnumerable<PhotoGalleryResponse>> GetAllAsync()
        {
            HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync("api/AwsPhotoGallery");

            await httpResponseMessage.ThrowIfResponseHasInvalidStatusCode();

            return await JsonSerializer.DeserializeAsync<IEnumerable<PhotoGalleryResponse>>(
                await httpResponseMessage.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }

        public async Task InsertAsync(InsertPhotoGralleryRequest insertPhotoGralleryRequest)
        {
            StringContent stringContent = GetStringContent(insertPhotoGralleryRequest);

            HttpResponseMessage httpResponseMessage = await HttpClient.PostAsync("api/AwsPhotoGallery", stringContent);

            await httpResponseMessage.ThrowIfResponseHasInvalidStatusCode();
        }

        public async Task DeleteAsync(DeleteRequest deleteRequest)
        {
            StringContent stringContent = GetStringContent(deleteRequest);

            HttpResponseMessage httpResponseMessage = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, "api/AwsPhotoGallery")
            {
                Content = stringContent
            });

            await httpResponseMessage.ThrowIfResponseHasInvalidStatusCode();
        }

        private StringContent GetStringContent(object valueToSend)
        {
            return new StringContent(
                JsonSerializer.Serialize(valueToSend),
                Encoding.UTF8, "application/json");
        }
    }
}