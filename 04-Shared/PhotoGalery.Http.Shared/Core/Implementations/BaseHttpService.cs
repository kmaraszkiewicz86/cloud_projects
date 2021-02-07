using System;
using System.Net.Http;

namespace PhotoGalery.Http.Shared.Core.Implementations
{
    public abstract class BaseHttpService
    {
        private readonly string BaseAdress = "http://localhost:5000";

        protected readonly HttpClient HttpClient;

        protected BaseHttpService(HttpClient httpClient)
        {
            HttpClient = httpClient;
            HttpClient.BaseAddress = new Uri(BaseAdress);
        }
    }
}