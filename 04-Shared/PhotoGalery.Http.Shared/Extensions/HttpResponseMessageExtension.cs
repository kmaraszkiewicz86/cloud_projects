using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PhotoGallery.Shared.ApiModels.Api;
using PhotoGallery.Shared.Exceptions;

namespace PhotoGalery.Http.Shared.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public static async Task ThrowIfResponseHasInvalidStatusCode(
            this HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
                {
                    var errorResponse = await httpResponseMessage.SerializeAsync<ErrorResponse>();
                    
                    throw new InvalidResponseException($"{errorResponse.ErrorMessage}");
                }
                
                if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorValidationResponse = await httpResponseMessage.SerializeAsync<ErrorValidationResponse>();

                    if (errorValidationResponse.ModelStateErrors != null && errorValidationResponse.ModelStateErrors.Any())
                    {
                        throw new InvalidResponseException(errorValidationResponse.ModelStateErrors);
                    }
                    
                    var errorResponse = await httpResponseMessage.SerializeAsync<ErrorResponse>();
                    
                    throw new InvalidResponseException($"{errorResponse.ErrorMessage}");
                }

                string invalidResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                throw new InvalidResponseException($"[{httpResponseMessage.StatusCode}]: {invalidResponse}");
            }
        }

        private static async Task<TReturn> SerializeAsync<TReturn>(this HttpResponseMessage httpResponseMessage)
            where TReturn: new()
        {
            return JsonSerializer.Deserialize<TReturn>(
                await httpResponseMessage.Content.ReadAsStringAsync(),
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }
    }
}