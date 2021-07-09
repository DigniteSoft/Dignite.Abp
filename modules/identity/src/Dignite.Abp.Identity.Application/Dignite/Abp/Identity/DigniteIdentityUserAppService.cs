using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace Dignite.Abp.Identity
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IIdentityUserAppService), typeof(IdentityUserAppService), typeof(DigniteIdentityUserAppService))]

    public class DigniteIdentityUserAppService: IdentityUserAppService
    {
        public DigniteIdentityUserAppService(
            IdentityUserManager userManager,
            IIdentityUserRepository userRepository,
            IIdentityRoleRepository roleRepository,
            IOptions<IdentityOptions> identityOptions
       ) : base(
           userManager,
           userRepository,
           roleRepository,
           identityOptions
           )
        {
        }


        [Authorize(IdentityPermissions.UserLookup.Default)]
        public override async Task<ListResultDto<Volo.Abp.Identity.IdentityRoleDto>> GetRolesAsync(Guid id)
        {
            var roles = await UserRepository.GetRolesAsync(id);

            return new ListResultDto<Volo.Abp.Identity.IdentityRoleDto>(
                ObjectMapper.Map<List<IdentityRole>, List<Volo.Abp.Identity.IdentityRoleDto>>(roles)
            );
        }
    }
}
