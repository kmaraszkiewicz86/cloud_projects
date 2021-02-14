using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PhotoGalery.Http.Shared.Core.Interfaces;
using PhotoGalery.Http.Shared.Extensions;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;

namespace PhotoGalery.Http.Shared.Core.Implementations
{
    public class UploadPhotoHttpService : BaseHttpService, IUploadPhotoHttpService
    {
        public UploadPhotoHttpService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<UploadedPhotoResponse> UploadAsync(string id, Stream photoStream)
        {
            var streamContent = new StreamContent(photoStream);

            HttpResponseMessage httpResponseMessage = await HttpClient.PutAsync($"api/AwsPhotoGallery/UploadPhoto/{id}",
                streamContent);

            await httpResponseMessage.ThrowIfResponseHasInvalidStatusCode();

            string jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UploadedPhotoResponse>(jsonString,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}