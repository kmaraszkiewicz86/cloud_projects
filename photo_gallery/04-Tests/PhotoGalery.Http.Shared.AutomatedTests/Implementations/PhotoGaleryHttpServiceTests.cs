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
        public async Task InsertAsync_WhenUserFetchingDataFromServerAndGetValidResponse()
        {
            MockSendAsyncMethod(HttpStatusCode.OK);
            
            await _serviceUnderTest.InsertAsync(new InsertPhotoGralleryRequest());
        }
        
        [Test]
        public async Task InsertAsync_WhenUserFetchingDataFromServerAndGetInvalidResponseWithModelStateErrors()
        {
            var expectedInvalidResponseException = new InvalidResponseException(new Dictionary<string, string[]>
            {
                {"Name", new[] {"The Name field is required."}}
            });

            await TestSendingRequestAndThenResponseHasErrors(
                expectedInvalidResponseException,
                "ModelStateErrorResponseTestData",
                async () => await _serviceUnderTest.InsertAsync(new InsertPhotoGralleryRequest()));
        }
        
        [Test]
        public async Task InsertAsync_WhenUserFetchingDataFromServerAndGetInvalidResponse()
        {
            var expectedInvalidResponseException = new InvalidResponseException("The error message");

            await TestSendingRequestAndThenResponseHasErrors(
                expectedInvalidResponseException,
                "ErrorMessageJsonRespone",
                async () => await _serviceUnderTest.InsertAsync(new InsertPhotoGralleryRequest()));
        }
        
        [Test]
        public async Task DeleteAsync_WhenUserFetchingDataFromServerAndGetValidResponse()
        {
            MockSendAsyncMethod(HttpStatusCode.OK);
            
            await _serviceUnderTest.DeleteAsync(new DeleteRequest { Id = "1" });
        }
        
        [Test]
        public async Task DeleteAsync_WhenUserFetchingDataFromServerAndGetInvalidResponseWithModelStateErrors()
        {
            var expectedInvalidResponseException = new InvalidResponseException(new Dictionary<string, string[]>
            {
                {"Name", new[] {"The Name field is required."}}
            });

            await TestSendingRequestAndThenResponseHasErrors(
                expectedInvalidResponseException,
                "ModelStateErrorResponseTestData",
                async () => await _serviceUnderTest.DeleteAsync(new DeleteRequest {Id = "1"}));
        }
        
        [Test]
        public async Task DeleteAsync_WhenUserFetchingDataFromServerAndGetInvalidResponse()
        {
            var expectedInvalidResponseException = new InvalidResponseException("The error message");

            await TestSendingRequestAndThenResponseHasErrors(
                expectedInvalidResponseException,
                "ErrorMessageJsonRespone",
                async () => await _serviceUnderTest.DeleteAsync(new DeleteRequest { Id = "1" }));
        }
        
        private async Task TestSendingRequestAndThenResponseHasErrors(
            InvalidResponseException expectedInvalidResponseException,
            string jsonFilename,
            Func<Task> doHttpRequestWorkAsync)
        {
            MockSendAsyncMethod(HttpStatusCode.BadRequest, GetContent(jsonFilename));

            try
            {
                await doHttpRequestWorkAsync();
                
                throw new Exception(
                    $"Expected that the method {nameof(PhotoGaleryHttpService.GetAllAsync)} throws error, but noting happen");
            }
            catch (InvalidResponseException invalidResponseException)
            {
                invalidResponseException.ModelStateErrors.Should().BeEquivalentTo(expectedInvalidResponseException.ModelStateErrors);
                invalidResponseException.Message.Should().Be(expectedInvalidResponseException.Message);
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
            HttpContent content = null)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = statusCode
            };

            if (content != null)
            {
                httpResponseMessage.Content = content;
            }
            
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);
        }
    }
}