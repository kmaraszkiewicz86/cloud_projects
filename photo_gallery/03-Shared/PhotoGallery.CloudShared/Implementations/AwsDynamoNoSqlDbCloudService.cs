using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Microsoft.Extensions.Options;
using PhotoGallery.CloudShared.Interfaces;
using PhotoGallery.Shared.Exceptions;
using PhotoGallery.Shared.Models;

namespace PhotoGallery.CloudShared.Implementations
{
    public class AwsDynamoNoSqlDbCloudService : INoSqlDbCloudService
    {
        private readonly string _tableName = "PhotoGallery";
        
        private DynamoDBContext Context => new DynamoDBContext(_dynamoDbClient);
        
        private readonly AmazonDynamoDBClient _dynamoDbClient;

        public AwsDynamoNoSqlDbCloudService(IOptions<AwsConfigurations> awsConfigurationsOptions)
        {
            BasicAWSCredentials awsCredentials = GetAwsCredentials(awsConfigurationsOptions);
            
            _dynamoDbClient = new AmazonDynamoDBClient(awsCredentials, RegionEndpoint.EUWest3);
        }

        private BasicAWSCredentials GetAwsCredentials(IOptions<AwsConfigurations> awsConfigurationsOptions)
        {
            AwsConfigurations awsConfigurations = awsConfigurationsOptions.Value;
            
            return new BasicAWSCredentials(awsConfigurations.AccessKey, 
                awsConfigurations.SecretKey); 
        }

        public async Task InsertAsync(PhotoGalleryModel photoGalleryModel)
        {
            await CreateTableIfNotExistsAsync();
            await WaitForTableWillBeReadyToModifyAsync();

            await Context.SaveAsync(photoGalleryModel);
        }
        
        public async Task DeleteAsync(string id)
        {
            await CreateTableIfNotExistsAsync();
            await WaitForTableWillBeReadyToModifyAsync();
            
            PhotoGalleryModel photoGalleryModelToDelete = await FindAsync(id);
            await Context.DeleteAsync<PhotoGalleryModel>(photoGalleryModelToDelete);
        }

        public async Task<IEnumerable<PhotoGalleryModel>> GetAllAsync()
        {
            await CreateTableIfNotExistsAsync();
            await WaitForTableWillBeReadyToModifyAsync();
            
            return await Context.ScanAsync<PhotoGalleryModel>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task<PhotoGalleryModel> FindAsync(string id)
        {
            await CreateTableIfNotExistsAsync();
            await WaitForTableWillBeReadyToModifyAsync();
            
            return await Context.LoadAsync<PhotoGalleryModel>(id);
        }

        private async Task WaitForTableWillBeReadyToModifyAsync()
        {
            try
            {
                TableStatus status = TableStatus.DELETING;

                do
                {
                    await Task.Delay(200);

                    DescribeTableResponse response = await _dynamoDbClient.DescribeTableAsync(new DescribeTableRequest
                    {
                        TableName = _tableName
                    });

                    status = response.Table.TableStatus;
                } while (status != TableStatus.ACTIVE);
            }
            catch (ResourceNotFoundException resourceNotFoundException)
            {
                throw new BadRequestException(resourceNotFoundException.Message);
            }
        }
        
        private async Task CreateTableIfNotExistsAsync()
        {
            List<string> currentTables = (await _dynamoDbClient.ListTablesAsync()).TableNames;

            if (currentTables.Contains(_tableName))
            {
                return;
            }

            var request = new CreateTableRequest
            {
                TableName = _tableName,
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                    new AttributeDefinition
                    {
                        AttributeName = "Id",
                        AttributeType = "S"
                    }
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "Id",
                        KeyType = "Hash"
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    WriteCapacityUnits = 5,
                    ReadCapacityUnits = 5,

                }
            };

            try
            {
                CreateTableResponse response = await _dynamoDbClient.CreateTableAsync(request);
                Debug.WriteLine(response.ResponseMetadata.RequestId);
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
        }
    }
}