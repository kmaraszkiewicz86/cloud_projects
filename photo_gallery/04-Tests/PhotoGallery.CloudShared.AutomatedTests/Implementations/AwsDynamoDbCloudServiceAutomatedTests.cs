using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using PhotoGallery.CloudShared.Implementations;
using PhotoGallery.Shared.Models;

namespace PhotoGallery.CloudShared.Tests.Implementations
{
    public class AwsDynamoDbCloudServiceAutomatedTest
    {
        private readonly AwsDynamoDbCloudService _awsDynamoDbCloudService;

        public AwsDynamoDbCloudServiceAutomatedTest()
        {
            var optionsMock = new Mock<IOptions<AwsConfigurations>>();
            optionsMock.SetupGet(mock => mock.Value).Returns(new AwsConfigurations
            {
                AccessKey = "AKIA2NEL25CEZJZ4YQJJ",
                SecretKey = "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"
            });
            
            _awsDynamoDbCloudService = new AwsDynamoDbCloudService(optionsMock.Object);
        }

        [Test]
        public async Task InsertItemTest()
        {
            try
            {
                await _awsDynamoDbCloudService.InsertAsync(new PhotoGalleryModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "TestItem",
                    BucketName = "BucketNameTest1",
                    KeyName = "KeyName1"
                });

                IEnumerable<PhotoGalleryModel> photoGalleryModels = await _awsDynamoDbCloudService.GetAllAsync();

                foreach (PhotoGalleryModel photoGalleryModel in photoGalleryModels)
                {
                    Console.WriteLine(photoGalleryModel.ToString());
                    
                    await _awsDynamoDbCloudService.DeleteAsync(photoGalleryModel.Id);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}