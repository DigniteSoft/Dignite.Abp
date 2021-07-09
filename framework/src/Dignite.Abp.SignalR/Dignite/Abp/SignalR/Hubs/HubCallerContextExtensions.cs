using Dignite.Abp.RealTime;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Security.Claims;

namespace Dignite.Abp.SignalR.Dignite.Abp.SignalR.Hubs
{
    public static class HubCallerContextExtensions
    {
        public static Guid? GetTenantId(this HubCallerContext context)
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

            return Guid.Parse(tenantIdClaim.Value);
        }

        public static Guid? GetUserIdOrNull(this HubCallerContext context)
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

            if (!Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return null;
            }

            return userId;
        }

        public static Guid GetUserId(this HubCallerContext context)
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
