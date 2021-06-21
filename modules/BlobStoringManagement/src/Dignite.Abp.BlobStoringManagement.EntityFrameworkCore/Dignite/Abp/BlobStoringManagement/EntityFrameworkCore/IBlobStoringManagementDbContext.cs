using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.BlobStoringManagement.EntityFrameworkCore
{
    [ConnectionStringName(BlobStoringManagementDbProperties.ConnectionStringName)]
    public interface IBlobStoringManagementDbContext : IEfCoreDbContext
    {
        DbSet<Blob> Blobs { get; }         
    }
}