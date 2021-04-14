using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring
{
    public class AuthorizationHandlerConfiguration
    {
        public AuthorizationOperations Operations
        {
            get => _containerConfiguration.GetConfigurationOrDefault(AuthorizationHandlerConfigurationNames.AuthorizationOperations, AuthorizationOperations.Saving|AuthorizationOperations.Getting|AuthorizationOperations.Deleting);
            set => _containerConfiguration.SetConfiguration(AuthorizationHandlerConfigurationNames.AuthorizationOperations, value);
        }

        public string Policy
        {
            get => _containerConfiguration.GetConfigurationOrDefault<string>(AuthorizationHandlerConfigurationNames.AuthorizationPolicy, null);
            set => _containerConfiguration.SetConfiguration(AuthorizationHandlerConfigurationNames.AuthorizationPolicy, value);
        }

        public string[] Roles
        {
            get => _containerConfiguration.GetConfigurationOrDefault<string[]>(AuthorizationHandlerConfigurationNames.AuthorizationRoles, null);
            set => _containerConfiguration.SetConfiguration(AuthorizationHandlerConfigurationNames.AuthorizationRoles, value);
        }

        private readonly BlobContainerConfiguration _containerConfiguration;

        public AuthorizationHandlerConfiguration(BlobContainerConfiguration containerConfiguration)
        {
            _containerConfiguration = containerConfiguration;
        }
    }
}
