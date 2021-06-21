using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace Dignite.Abp.BlobStoring
{
    public class AuthorizationHandler:IAuthorizationHandler
    {
        public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

        protected ICurrentUser CurrentUser => LazyServiceProvider.LazyGetRequiredService<ICurrentUser>();
        protected IAuthorizationService AuthorizationService => LazyServiceProvider.LazyGetRequiredService<IAuthorizationService>();

        public virtual async Task CheckSavingPermissionAsync(AuthorizationHandlerConfiguration configuration)
        {
            if (!CurrentUser.IsAuthenticated)
            {
                // TODO: 考虑异常改为 BusinessException
                throw new Volo.Abp.Authorization.AbpAuthorizationException("未授权");
                // throw new BusinessException(
                //     code: "Dignite.Abp.BlobStoring:010001",
                //     message: "Unauthorized!",
                //     details: "Current user is not authorized!"
                // );
            }
            else if (!configuration.SavingPolicy.IsNullOrEmpty() && !await AuthorizationService.IsGrantedAsync(configuration.SavingPolicy))
            {
                // TODO: 考虑异常改为 BusinessException
                throw new Volo.Abp.Authorization.AbpAuthorizationException("未授权");
            }
            else if (configuration.SavingRoles != null && configuration.SavingRoles.Any() && !CurrentUser.Roles.Intersect(configuration.SavingRoles).Any())
            {
                // TODO: 考虑异常改为 BusinessException
                throw new Volo.Abp.Authorization.AbpAuthorizationException("未授权");
            }
        }



        public virtual async Task CheckGettingPermissionAsync(AuthorizationHandlerConfiguration configuration, IBlobInfo blobInfo)
        {
            if (!configuration.GettingPolicy.IsNullOrEmpty() && !await AuthorizationService.IsGrantedAsync(configuration.GettingPolicy))
            {
                // TODO: 考虑异常改为 BusinessException
                throw new Volo.Abp.Authorization.AbpAuthorizationException("未授权");
            }
            else if (configuration.GettingRoles != null && configuration.GettingRoles.Any() && !CurrentUser.Roles.Intersect(configuration.GettingRoles).Any())
            {
                // TODO: 考虑异常改为 BusinessException
                throw new Volo.Abp.Authorization.AbpAuthorizationException("未授权");
            }
        }


        public virtual async Task CheckDeletingPermissionAsync(AuthorizationHandlerConfiguration configuration, IBlobInfo blobInfo)
        {
            if (!CurrentUser.IsAuthenticated)
            {
                // TODO: 考虑异常改为 BusinessException
                throw new Volo.Abp.Authorization.AbpAuthorizationException("未授权");
                // throw new BusinessException(
                //     code: "Dignite.Abp.BlobStoring:010001",
                //     message: "Unauthorized!",
                //     details: "Current user is not authorized!"
                // );
            }
            else if (!configuration.DeletingPolicy.IsNullOrEmpty() && !await AuthorizationService.IsGrantedAsync(configuration.DeletingPolicy))
            {
                // TODO: 考虑异常改为 BusinessException
                throw new Volo.Abp.Authorization.AbpAuthorizationException("未授权");
            }
            else if (configuration.DeletingRoles != null && configuration.DeletingRoles.Any() && !CurrentUser.Roles.Intersect(configuration.DeletingRoles).Any())
            {
                // TODO: 考虑异常改为 BusinessException
                throw new Volo.Abp.Authorization.AbpAuthorizationException("未授权");
            }
        }
    }
}
