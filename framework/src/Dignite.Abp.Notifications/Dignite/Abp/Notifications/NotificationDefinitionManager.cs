using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Features;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Claims;

namespace Dignite.Abp.Notifications
{
    /// <summary>
    /// Implements <see cref="INotificationDefinitionManager"/>.
    /// </summary>
    internal class NotificationDefinitionManager : INotificationDefinitionManager, ISingletonDependency
    {
        protected Lazy<IDictionary<string, NotificationDefinition>> NotificationDefinitions { get; }

        protected NotificationOptions Options { get; }

        protected IFeatureChecker FeatureChecker { get; }

        protected IServiceProvider ServiceProvider { get; }

        protected IAuthorizationService AuthorizationService { get; }

        public NotificationDefinitionManager(
            IOptions<NotificationOptions> options,
            IServiceProvider serviceProvider,
            IFeatureChecker featureChecker,
            IAuthorizationService authorizationService
            )
        {
            ServiceProvider = serviceProvider;
            Options = options.Value;
            FeatureChecker = featureChecker;
            AuthorizationService = authorizationService;
            NotificationDefinitions = new Lazy<IDictionary<string, NotificationDefinition>>(CreateNotificationDefinitions, true);
        }

        public virtual NotificationDefinition Get(string name)
        {
            Check.NotNull(name, nameof(name));

            var setting = GetOrNull(name);

            if (setting == null)
            {
                throw new AbpException("Undefined notification: " + name);
            }

            return setting;
        }

        public virtual IReadOnlyList<NotificationDefinition> GetAll()
        {
            return NotificationDefinitions.Value.Values.ToImmutableList();
        }

        public virtual NotificationDefinition GetOrNull(string name)
        {
            return NotificationDefinitions.Value.GetOrDefault(name);
        }

        protected virtual IDictionary<string, NotificationDefinition> CreateNotificationDefinitions()
        {
            var settings = new Dictionary<string, NotificationDefinition>();

            using (var scope = ServiceProvider.CreateScope())
            {
                var providers = Options
                    .DefinitionProviders
                    .Select(p => scope.ServiceProvider.GetRequiredService(p) as INotificationDefinitionProvider)
                    .ToList();

                foreach (var provider in providers)
                {
                    provider.Define(new NotificationDefinitionContext(settings));
                }
            }

            return settings;
        }
        public async Task<bool> IsAvailableAsync(string name)
        {
            var notificationDefinition = GetOrNull(name);
            if (notificationDefinition == null)
            {
                return true;
            }

            return (await FeatureCheckAsync(notificationDefinition)
                && await PermissionCheckAsync(notificationDefinition)
                );
        }


        protected async Task<bool> FeatureCheckAsync(NotificationDefinition notificationDefinition)
        {
            if (notificationDefinition.FeatureName != null)
            {
                var result = await FeatureChecker.GetAsync(notificationDefinition.FeatureName,false);
                if (!result)
                {
                    return false;
                }
            }
            return true;
        }

        protected async Task<bool> PermissionCheckAsync(NotificationDefinition notificationDefinition)
        {
            if (!notificationDefinition.PermissionName.IsNullOrEmpty())
            {
                    var result = await AuthorizationService.AuthorizeAsync(notificationDefinition.PermissionName);
                    if (!result.Succeeded)
                    {
                        return false;
                    }
            }
            return true;
        }



        public async Task<IReadOnlyList<NotificationDefinition>> GetAllAvailableAsync()
        {
            var availableDefinitions = new List<NotificationDefinition>();

            foreach (var notificationDefinition in GetAll())
            {
                if (await FeatureCheckAsync(notificationDefinition)
                    && await PermissionCheckAsync(notificationDefinition))
                {
                    availableDefinitions.Add(notificationDefinition);
                }
            }
            return availableDefinitions.ToImmutableList();
        }
    }
}
