using System;
using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring
{
    public static class AuthorizationHandlerConfigurationExtensions
    {
        public static AuthorizationHandlerConfiguration GetGeneralAuthorizationConfiguration(
            this BlobContainerConfiguration containerConfiguration)
        {
            return new AuthorizationHandlerConfiguration(containerConfiguration);
        }

        public static void SetAuthorizationHandler<TAuthorizationHandler>(
            this BlobContainerConfiguration containerConfiguration,
            Action<AuthorizationHandlerConfiguration> configureAction)
            where TAuthorizationHandler : class, IAuthorizationHandler
        {
            containerConfiguration.SetConfiguration(
                DigniteAbpBlobContainerConfigurationNames.AuthorizationHandler,
                typeof(TAuthorizationHandler));

            //
            configureAction(new AuthorizationHandlerConfiguration(containerConfiguration));
        }
    }
}
