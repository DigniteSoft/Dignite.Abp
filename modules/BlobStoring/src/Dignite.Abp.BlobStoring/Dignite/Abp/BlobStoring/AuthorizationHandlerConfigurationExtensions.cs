using System;
using Volo.Abp.BlobStoring;
using Volo.Abp.Collections;

namespace Dignite.Abp.BlobStoring
{
    public static class AuthorizationHandlerConfigurationExtensions
    {
        public static AuthorizationHandlerConfiguration GetGeneralAuthorizationConfiguration(
            this BlobContainerConfiguration containerConfiguration)
        {
            return new AuthorizationHandlerConfiguration(containerConfiguration);
        }

        public static void AddAuthorizationHandler<TAuthorizationHandler>(
            this BlobContainerConfiguration containerConfiguration,
            Action<AuthorizationHandlerConfiguration> configureAction)
            where TAuthorizationHandler:IAuthorizationHandler
        {
            var authorizationHandlers = containerConfiguration.GetConfigurationOrDefault(
                DigniteAbpBlobContainerConfigurationNames.AuthorizationHandlers,
                new TypeList<IAuthorizationHandler>());

            if (authorizationHandlers.TryAdd<TAuthorizationHandler>())
            {
                configureAction(new AuthorizationHandlerConfiguration(containerConfiguration));
            }
        }
    }
}
