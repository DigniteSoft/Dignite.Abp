using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.BlobStoringManagement.MongoDB
{
    [ConnectionStringName(BlobStoringManagementDbProperties.ConnectionStringName)]
    public class BlobStoringManagementMongoDbContext : AbpMongoDbContext, IBlobStoringManagementMongoDbContext
    {
        public IMongoCollection<Blob> Blobs => Collection<Blob>();
         

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureBlobStoringManagement();
        }
    }
}