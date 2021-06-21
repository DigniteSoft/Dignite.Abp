using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring
{
    public class AuthorizationHandlerConfiguration
    {
        public string SavingPolicy
        {
            get => _containerConfiguration.GetConfigurationOrDefault<string>(AuthorizationHandlerConfigurationNames.SavingAuthorizationPolicy, null);
            set => _containerConfiguration.SetConfiguration(AuthorizationHandlerConfigurationNames.SavingAuthorizationPolicy, value);
        }

        public string[] SavingRoles
        {
            get => _containerConfiguration.GetConfigurationOrDefault<string[]>(AuthorizationHandlerConfigurationNames.SavingAuthorizationRoles, null);
            set => _containerConfiguration.SetConfiguration(AuthorizationHandlerConfigurationNames.SavingAuthorizationRoles, value);
        }
        public string GettingPolicy
        {
            get => _containerConfiguration.GetConfigurationOrDefault<string>(AuthorizationHandlerConfigurationNames.GettingAuthorizationPolicy, null);
            set => _containerConfiguration.SetConfiguration(AuthorizationHandlerConfigurationNames.GettingAuthorizationPolicy, value);
        }

        public string[] GettingRoles
        {
            get => _containerConfiguration.GetConfigurationOrDefault<string[]>(AuthorizationHandlerConfigurationNames.GettingAuthorizationRoles, null);
            set => _containerConfiguration.SetConfiguration(AuthorizationHandlerConfigurationNames.GettingAuthorizationRoles, value);
        }
        public string DeletingPolicy
        {
            get => _containerConfiguration.GetConfigurationOrDefault<string>(AuthorizationHandlerConfigurationNames.DeletingAuthorizationPolicy, null);
            set => _containerConfiguration.SetConfiguration(AuthorizationHandlerConfigurationNames.DeletingAuthorizationPolicy, value);
        }

        public string[] DeletingRoles
        {
            get => _containerConfiguration.GetConfigurationOrDefault<string[]>(AuthorizationHandlerConfigurationNames.DeletingAuthorizationRoles, null);
            set => _containerConfiguration.SetConfiguration(AuthorizationHandlerConfigurationNames.DeletingAuthorizationRoles, value);
        }

        private readonly BlobContainerConfiguration _containerConfiguration;

        public AuthorizationHandlerConfiguration(BlobContainerConfiguration containerConfiguration)
        {
            _containerConfiguration = containerConfiguration;
        }
    }
}
