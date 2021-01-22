using System.IO;
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
                    

                    throw new InvalidResponseException($"[{httpResponseMessage.StatusCode}]: " +
                                                       $"{errorResponse.ErrorMessage}");
                }
                
                if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorValidationResponse = await httpResponseMessage.SerializeAsync<ErrorValidationResponse>();

                    if (errorValidationResponse != null)
                    {
                        throw new InvalidResponseException(errorValidationResponse.ModelStateErrors);
                    }
                    
                    var errorResponse = await httpResponseMessage.SerializeAsync<ErrorResponse>();
                    
                    throw new InvalidResponseException($"[{httpResponseMessage.StatusCode}]: {errorResponse.ErrorMessage}");
                }
                
                throw new InvalidResponseException($"[{httpResponseMessage.StatusCode}]: Unknown error");
                
            }
        }

        private static async Task<TReturn> SerializeAsync<TReturn>(this HttpResponseMessage httpResponseMessage)
            where TReturn: new()
        {
            return await JsonSerializer.DeserializeAsync<TReturn>(
                await httpResponseMessage.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }
    }
}