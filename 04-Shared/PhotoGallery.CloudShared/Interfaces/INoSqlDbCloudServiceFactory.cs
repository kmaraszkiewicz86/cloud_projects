namespace PhotoGallery.CloudShared.Interfaces
{
    public interface INoSqlDbCloudServiceFactory
    {
        INoSqlDbCloudService GetAwsDynamoNoSqlDbCloudService();
    }
}