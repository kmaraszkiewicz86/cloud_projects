using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PhotoGalery.Http.Shared.Extensions;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;

namespace PhotoGalery.Http.Shared.Core.Implementations
{
    public class PhotoGaleryHttpService
    {
        private readonly string BaseAdress = "http://192.168.0.23:5000/api/AwsPhotoGallery";

        private readonly HttpClient _httpClient;

        public PhotoGaleryHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseAdress);
        }

        public async Task<IEnumerable<PhotoGalleryResponse>> GetAllAsync()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("");

            await httpResponseMessage.ThrowIfResponseHasInvalidStatusCode();

            return await JsonSerializer.DeserializeAsync<IEnumerable<PhotoGalleryResponse>>(
                await httpResponseMessage.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }

        public async Task InsertAsync(InsertPhotoGralleryRequest insertPhotoGralleryRequest)
        {
            StringContent stringContent = GetStringContent(insertPhotoGralleryRequest);

            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync("", stringContent);

            await httpResponseMessage.ThrowIfResponseHasInvalidStatusCode();
        }

        public async Task DeleteAsync(DeleteRequest deleteRequest)
        {
            StringContent stringContent = GetStringContent(deleteRequest);

            HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(new HttpRequestMessage
            {
                Content = stringContent,
                Method = HttpMethod.Delete,
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