using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Dignite.Abp.BlobStoringManagement.EntityFrameworkCore
{
    public static class BlobStoringManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureBlobStoringManagement(
            this ModelBuilder builder,
            Action<BlobStoringManagementModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new BlobStoringManagementModelBuilderConfigurationOptions(
                BlobStoringManagementDbProperties.DbTablePrefix,
                BlobStoringManagementDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);


            builder.Entity<Blob>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Blobs", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.EntityType).IsRequired().HasMaxLength(BlobConsts.MaxEntityTypeLength);
                b.Property(q => q.EntityId).IsRequired().HasMaxLength(BlobConsts.MaxEntityIdLength);
                b.Property(q => q.ContainerName).IsRequired().HasMaxLength(BlobConsts.MaxContainerNameLength);
                b.Property(q => q.BlobName).IsRequired().HasMaxLength(BlobConsts.MaxBlobNameLength);
                b.Property(q => q.Hash).HasMaxLength(BlobConsts.MaxBlobHashLength);
                b.Property(q => q.ReferBlobName).HasMaxLength(BlobConsts.MaxBlobNameLength);                
                
                

                //Indexes
                b.HasIndex(q => new {q.TenantId, q.EntityType,q.EntityId });
                b.HasIndex(q => new { q.TenantId, q.ContainerName, q.BlobName });
                b.HasIndex(q => new { q.TenantId, q.ContainerName, q.ReferBlobName });
                b.HasIndex(q => new { q.TenantId, q.ContainerName, q.Hash });
                b.HasIndex(q => new { q.TenantId, q.CreatorId });
            });
            
        }
    }
}