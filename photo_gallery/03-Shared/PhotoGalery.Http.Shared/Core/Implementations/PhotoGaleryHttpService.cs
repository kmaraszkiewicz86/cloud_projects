using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PhotoGalery.Http.Shared.Extensions;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;

namespace PhotoGalery.Http.Shared.Core.Implementations
{
    public class PhotoGaleryHttpService
    {
        private readonly string BaseAdress = "http://192.168.0.23:5000/api";

        private readonly HttpClient _httpClient;

        public PhotoGaleryHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseAdress);
        }

        public async Task<IEnumerable<PhotoGalleryResponse>> GetAllAsync()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("/api/AwsPhotoGallery");

            await httpResponseMessage.ThrowIfResponseHasInvalidStatusCode();

            return await JsonSerializer.DeserializeAsync<IEnumerable<PhotoGalleryResponse>>(
                await httpResponseMessage.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }
    }
}