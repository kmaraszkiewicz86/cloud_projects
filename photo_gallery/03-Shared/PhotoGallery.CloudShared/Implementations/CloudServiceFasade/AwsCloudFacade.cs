using PhotoGallery.CloudShared.Interfaces;

namespace PhotoGallery.CloudShared.Implementations.CloudServiceFasade
{
    public class AwsCloudFacade : BaseCloudFacade, IAwsCloudFacade
    {
        public AwsCloudFacade(INoSqlDbCloudServiceFactory noSqlDbCloudServiceFactory) : base(noSqlDbCloudServiceFactory)
        {
        }

        protected override INoSqlDbCloudService GetNoSqlDbCloudService()
        {
            return NoSqlDbCloudServiceFactory.GetAwsDynamoNoSqlDbCloudService();
        }
    }
}