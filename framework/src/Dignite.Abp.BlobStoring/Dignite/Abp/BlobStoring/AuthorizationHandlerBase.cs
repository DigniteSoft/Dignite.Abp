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
    public abstract class AuthorizationHandlerBase:IAuthorizationHandler
    {
        public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

        protected ICurrentUser CurrentUser => LazyServiceProvider.LazyGetRequiredService<ICurrentUser>();
        protected IAuthorizationService AuthorizationService => LazyServiceProvider.LazyGetRequiredService<IAuthorizationService>();

        public async Task CheckAsync(AuthorizationOperations operation, BlobContainerConfiguration containerConfiguration)
        {
            var configuration = containerConfiguration.GetGeneralAuthorizationConfiguration();
            if (!configuration.Operations.HasFlag(operation))
            {
                await Task.CompletedTask;
            }
            else
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
                else if (!configuration.Policy.IsNullOrEmpty() && !await AuthorizationService.IsGrantedAsync(configuration.Policy))
                {
                    // TODO: 考虑异常改为 BusinessException
                    throw new Volo.Abp.Authorization.AbpAuthorizationException("未授权");
                }
                else if (configuration.Roles != null && configuration.Roles.Any() && !CurrentUser.Roles.Intersect(configuration.Roles).Any())
                {
                    // TODO: 考虑异常改为 BusinessException
                    throw new Volo.Abp.Authorization.AbpAuthorizationException("未授权");
                }

                // 进一步验证基于资源的权限
                await CheckResourcePermissionAsync(operation);
            }
        }

        /// <summary>
        /// 基于资源的权限验证
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        protected abstract Task CheckResourcePermissionAsync(AuthorizationOperations operation);
    }
}
