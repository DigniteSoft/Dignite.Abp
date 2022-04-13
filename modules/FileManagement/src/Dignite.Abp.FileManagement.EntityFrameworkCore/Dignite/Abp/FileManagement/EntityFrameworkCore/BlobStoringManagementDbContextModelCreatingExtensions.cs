using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Dignite.Abp.FileManagement.EntityFrameworkCore
{
    public static class FileManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureFileManagement(
            this ModelBuilder builder,
            Action<FileManagementModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new FileManagementModelBuilderConfigurationOptions(
                FileManagementDbProperties.DbTablePrefix,
                FileManagementDbProperties.DbSchema
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
                b.Property(q => q.BlobFileName).HasMaxLength(BlobConsts.MaxBlobFileNameLength);



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