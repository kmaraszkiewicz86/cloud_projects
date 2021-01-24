using System;
using System.Net.Http;

namespace PhotoGalery.Http.Shared.Core.Implementations
{
    public abstract class BaseHttpService
    {
        private readonly string BaseAdress = "http://192.168.0.23:5000/api/AwsPhotoGallery";

        protected readonly HttpClient HttpClient;

        protected BaseHttpService(HttpClient httpClient)
        {
            HttpClient = httpClient;
            HttpClient.BaseAddress = new Uri(BaseAdress);
        }
    }
}