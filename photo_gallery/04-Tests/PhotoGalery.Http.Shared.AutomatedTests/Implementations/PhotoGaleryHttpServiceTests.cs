using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using PhotoGalery.Http.Shared.Core.Implementations;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;
using PhotoGallery.Shared.Exceptions;

namespace PhotoGalery.Http.Shared.Tests.Implementations
{
    public class PhotoGaleryHttpServiceTests
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        
        private readonly PhotoGaleryHttpService _serviceUnderTest;

        public PhotoGaleryHttpServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            
            var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _serviceUnderTest = new PhotoGaleryHttpService(httpClient);
        }
        
        [Test]
        public async Task GetAllAsync_WhenUserFetchingDataFromServerAndGetValidResponse()
        {
            IEnumerable<PhotoGalleryResponse> expectedPhotoGalleryResponses = new List<PhotoGalleryResponse>
            {
                new PhotoGalleryResponse
                {
                    Id = "ID",
                    Name = "string"
                }
            };
            
            MockSendAsyncMethod(HttpStatusCode.OK, GetContent("GetResponseTestData"));
            
            IEnumerable<PhotoGalleryResponse> photoGalleryResponses = await _serviceUnderTest.GetAllAsync();

            photoGalleryResponses.Count().Should().BeGreaterThan(0);
            photoGalleryResponses.Should().BeEquivalentTo(expectedPhotoGalleryResponses);
        }
        
        [Test]
        public async Task GetAllAsync_WhenUserFetchingDataFromServerAndGetInvalidResponse()
        {
            var expectedInvalidResponseException = new InvalidResponseException(new Dictionary<string, string[]>
            {
                {"Name", new[] {"The Name field is required."}}
            });
            
            MockSendAsyncMethod(HttpStatusCode.BadRequest, GetContent("ModelStateErrorResponseTestData"));

            try
            {
                await _serviceUnderTest.GetAllAsync();

                throw new Exception(
                    $"Expected that the method {nameof(PhotoGaleryHttpService.GetAllAsync)} throws error, but noting happen");
            }
            catch (InvalidResponseException invalidResponseException)
            {
                invalidResponseException.ModelStateErrors.Should().BeEquivalentTo(
                    expectedInvalidResponseException.ModelStateErrors);
            }
        }

        private StringContent GetContent(string fileName)
        {
            return new (ReadJson(fileName),
                Encoding.UTF8,
                "application/json");
        }
        
        private string ReadJson(string fileName)
        {
            return File.ReadAllText($"TestData\\{fileName}.json");
        }

        private void MockSendAsyncMethod(HttpStatusCode statusCode,
            HttpContent content)
        {
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = content
                });
        }
    }
}