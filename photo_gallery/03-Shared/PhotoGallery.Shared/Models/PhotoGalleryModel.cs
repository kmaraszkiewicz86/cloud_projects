using System;
using Amazon.DynamoDBv2.DataModel;

namespace PhotoGallery.Shared.Models
{
    [DynamoDBTable("PhotoGallery")]
    public class PhotoGalleryModel
    {
        [DynamoDBHashKey]
        public string Id { get; set; }

        public string Name { get; set; }

        public string BucketName { get; set; }

        public string KeyName { get; set; }
        
        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}; " +
                   $"{nameof(Name)}: {Name}; " +
                   $"{nameof(BucketName)}: {BucketName}; " +
                   $"{nameof(KeyName)}: {KeyName};";
        }
    }
}