using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Security.Claims;

namespace Dignite.Abp.SignalR.Dignite.Abp.SignalR.Hubs
{
    public static class HubCallerContextExtensions
    {
        public static int? GetTenantId(this HubCallerContext context)
        {
            if (context?.User == null)
            {
                return null;
            }

            var tenantIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.TenantId);
            if (string.IsNullOrEmpty(tenantIdClaim?.Value))
            {
                return null;
            }

            return Convert.ToInt32(tenantIdClaim.Value);
        }

        public static long? GetUserIdOrNull(this HubCallerContext context)
        {
            if (context?.User == null)
            {
                return null;
            }

            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.UserId);
            if (string.IsNullOrEmpty(userIdClaim?.Value))
            {
                return null;
            }

            if (!long.TryParse(userIdClaim.Value, out var userId))
            {
                return null;
            }

            return userId;
        }

        public static long GetUserId(this HubCallerContext context)
        {
            var userId = context.GetUserIdOrNull();
            if (userId == null)
            {
                throw new AbpException("UserId is null! Probably, user is not logged in.");
            }

            return userId.Value;
        }

       
        public static UserIdentifier ToUserIdentifier(this HubCallerContext context)
        {
            var userId = context.GetUserIdOrNull();
            if (userId == null)
            {
                return null;
            }

            return new UserIdentifier(context.GetTenantId(), context.GetUserId());
        }
    }
}
