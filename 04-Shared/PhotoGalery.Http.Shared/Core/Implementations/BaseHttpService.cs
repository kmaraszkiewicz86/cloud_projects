using System;
using System.Net.Http;

namespace PhotoGalery.Http.Shared.Core.Implementations
{
    public abstract class BaseHttpService
    {
        private readonly string BaseAdress = "http://172.20.10.12:1234";

        protected readonly HttpClient HttpClient;

        protected BaseHttpService(HttpClient httpClient)
        {
            HttpClient = httpClient;
            HttpClient.BaseAddress = new Uri(BaseAdress);
        }
    }
}