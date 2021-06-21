using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.BlobStoringManagement.MongoDB
{
    [ConnectionStringName(BlobStoringManagementDbProperties.ConnectionStringName)]
    public interface IBlobStoringManagementMongoDbContext : IAbpMongoDbContext
    {
        IMongoCollection<Blob> Blobs { get; }
         
    }
}
