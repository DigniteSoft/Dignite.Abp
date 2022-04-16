using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dignite.Abp.FileManagement
{
    public class RouteFileEntityResolveContributor : HttpFileEntityResolveContributorBase
    {
        public const string ContributorName = "Route";

        public override string Name => ContributorName;

        protected override Task<FileEntityResolveResult> GetBlobEntityFromHttpContextOrNullAsync([NotNull] IFileEntityResolveContext context, [NotNull] HttpContext httpContext)
        {
            var entityType = httpContext.GetRouteValue(FileEntityResolverConsts.EntityTypeKey);
            var entityId = httpContext.GetRouteValue(FileEntityResolverConsts.EntityIdKey);

            if (entityType !=null && entityId!=null)
            {
                var result = new FileEntityResolveResult();
                result.EntityType = Convert.ToString(entityType);
                result.EntityId = Convert.ToString(entityId);

                return Task.FromResult(result);
            }

            return Task.FromResult((FileEntityResolveResult)null);
        }
    }
}
