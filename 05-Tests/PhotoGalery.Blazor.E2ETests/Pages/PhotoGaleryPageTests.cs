using System;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PhotoGalery.Http.Shared.Core.Interfaces;
using PhotoGalery.Pages;
using PhotoGalery.Shared.PhotoGaleryShared;
using Xunit;

namespace PhotoGalery.Blazor.E2ETests.Pages
{
    public class PhotoGaleryPageTests
    {
        [Fact]
        public void AddNewItemTest()
        {
            using (var context = new TestContext())
            {
                var mock = new Mock<IPhotoGaleryHttpService>();
                context.Services.AddSingleton<IPhotoGaleryHttpService>(mock.Object);

                var addPhotoGaleryPage = context.RenderComponent<AddPhotoGaleryPage>();
                //var photoGaleryPage = context.RenderComponent<PhotoGaleryPage>();
            }
        }
    }
}
