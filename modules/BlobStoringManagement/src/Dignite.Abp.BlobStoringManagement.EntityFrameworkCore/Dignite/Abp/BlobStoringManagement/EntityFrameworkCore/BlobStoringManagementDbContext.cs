using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.BlobStoringManagement.EntityFrameworkCore
{
    [ConnectionStringName(BlobStoringManagementDbProperties.ConnectionStringName)]
    public class BlobStoringManagementDbContext : AbpDbContext<BlobStoringManagementDbContext>, IBlobStoringManagementDbContext
    {
        public DbSet<Blob> Blobs { get; set; }
         

        public BlobStoringManagementDbContext(DbContextOptions<BlobStoringManagementDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureBlobStoringManagement();
        }
    }
}