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
            Action<AuthorizationHandlerConfiguration> configureAction=null)
            where TAuthorizationHandler : class, IAuthorizationHandler
        {
            containerConfiguration.SetConfiguration(
                DigniteAbpBlobContainerConfigurationNames.AuthorizationHandler,
                typeof(TAuthorizationHandler));

            //
            if (configureAction != null)
                configureAction(new AuthorizationHandlerConfiguration(containerConfiguration));
        }
    }
}
