using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Volo.Abp.Authorization.Permissions;
using System.Linq;
using Volo.Abp.Identity;

namespace Dignite.Abp.Identity
{
    public class OrganizationAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, OrganizationUnit>
    {
        private readonly IPermissionChecker _permissionChecker;
        private readonly IIdentityUserRepository _userRepository;

        public OrganizationAuthorizationHandler(IPermissionChecker permissionChecker, IIdentityUserRepository userRepository)
        {
            _permissionChecker = permissionChecker;
            _userRepository = userRepository;
        }

        protected async override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            OrganizationUnit resource)
        {
            if (requirement.Name == CommonOperations.Delete.Name && await HasDeletePermission(context, resource))
            {
                context.Succeed(requirement);
                return;
            }

            if (requirement.Name == CommonOperations.Update.Name && await HasUpdatePermission(context, resource))
            {
                context.Succeed(requirement);
                return;
            }

            if (requirement.Name == CommonOperations.Create.Name && await HasCreatePermission(context, resource))
            {
                context.Succeed(requirement);
                return;
            }
        }

        private async Task<bool> HasDeletePermission(AuthorizationHandlerContext context, OrganizationUnit resource)
        {
            if (await _permissionChecker.IsGrantedAsync(context.User, IdentityPermissions.OrganizationUnits.Delete))
            {
                if (await _permissionChecker.IsGrantedAsync(context.User, IdentityPermissions.OrganizationUnits.SuperAuthorization))
                    return true;
                else if (await AuthorizationCheck(context, resource))
                    return true;
            }

            return false;
        }

        private async Task<bool> HasUpdatePermission(AuthorizationHandlerContext context, OrganizationUnit resource)
        {
            if (await _permissionChecker.IsGrantedAsync(context.User, IdentityPermissions.OrganizationUnits.Update))
            {
                if (await _permissionChecker.IsGrantedAsync(context.User, IdentityPermissions.OrganizationUnits.SuperAuthorization))
                    return true;
                else if (await AuthorizationCheck(context, resource))
                    return true;
            }

            return false;
        }

        private async Task<bool> HasCreatePermission(AuthorizationHandlerContext context, OrganizationUnit parent)
        {
            if (await _permissionChecker.IsGrantedAsync(context.User, IdentityPermissions.OrganizationUnits.Create))
            {
                if (await _permissionChecker.IsGrantedAsync(context.User, IdentityPermissions.OrganizationUnits.SuperAuthorization))
                    return true;
                else if (await AuthorizationCheck(context, parent))
                    return true;
            }

            return false;
        }

        private async Task<bool> AuthorizationCheck(AuthorizationHandlerContext context, OrganizationUnit resource)
        {
            var userOrganizationUnits = await _userRepository.GetOrganizationUnitsAsync(context.User.FindUserId().Value);

            if (userOrganizationUnits.Any(ou => resource.Code.StartsWith(ou.Code)))
                return true;

            return false;
        }
    }
}