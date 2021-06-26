using System;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Dignite.Abp.Notifications.EntityFrameworkCore
{
    public static class NotificationsDbContextModelCreatingExtensions
    {
        public static void ConfigureNotifications(
            this ModelBuilder builder,
            Action<NotificationsModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new NotificationsModelBuilderConfigurationOptions(
                NotificationsDbProperties.DbTablePrefix,
                NotificationsDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            builder.Entity<NotificationSubscription>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "NotificationSubscriptions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(ns => ns.NotificationName).IsRequired().HasMaxLength(NotificationConsts.MaxNotificationNameLength);
                b.Property(ns => ns.EntityTypeName).HasMaxLength(NotificationConsts.MaxEntityTypeNameLength);
                b.Property(ns => ns.EntityId).HasMaxLength(NotificationConsts.MaxEntityIdLength);

                //Indexes
                b.HasIndex(ns => new object[] {
                    ns.TenantId,
                    ns.UserId,
                    ns.CreationTime
                });
            });

            builder.Entity<Notification>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Notifications", options.Schema);

                b.ConfigureByConvention();

                //Properties
                b.Property(n => n.NotificationName).IsRequired().HasMaxLength(NotificationConsts.MaxNotificationNameLength);
                b.Property(n => n.EntityTypeName).HasMaxLength(NotificationConsts.MaxEntityTypeNameLength);
                b.Property(n => n.EntityId).HasMaxLength(NotificationConsts.MaxEntityIdLength);
                b.Property(n => n.Data).IsRequired().HasConversion(
                    config => JsonConvert.SerializeObject(config),
                    jsonData => JsonConvert.DeserializeObject<NotificationData>(jsonData)
                    );

                //Indexes
                b.HasIndex(n => new object[] {
                    n.Id,
                    n.CreationTime
                });
            });



            builder.Entity<UserNotification>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "UserNotifications", options.Schema);

                b.ConfigureByConvention();


                //Relations
                b.HasOne(un => un.Notification).WithOne().IsRequired().HasForeignKey<UserNotification>(un => un.NotificationId);

                //Indexes
                b.HasIndex(un => un.UserId);
            });
        }
    }
}