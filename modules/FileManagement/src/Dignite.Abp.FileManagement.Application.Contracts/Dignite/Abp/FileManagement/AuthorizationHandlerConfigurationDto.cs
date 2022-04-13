

namespace Dignite.Abp.FileManagement
{
    public class AuthorizationHandlerConfigurationDto
    {
        public AuthorizationHandlerConfigurationDto(string savingPolicy, string[] savingRoles, string gettingPolicy, string[] gettingRoles, string deletingPolicy, string[] deletingRoles)
        {
            SavingPolicy = savingPolicy;
            SavingRoles = savingRoles;
            GettingPolicy = gettingPolicy;
            GettingRoles = gettingRoles;
            DeletingPolicy = deletingPolicy;
            DeletingRoles = deletingRoles;
        }

        public string SavingPolicy { get; }

        public string[] SavingRoles { get; }
        public string GettingPolicy { get; }

        public string[] GettingRoles { get; }
        public string DeletingPolicy { get; }

        public string[] DeletingRoles { get; }
    }
}
