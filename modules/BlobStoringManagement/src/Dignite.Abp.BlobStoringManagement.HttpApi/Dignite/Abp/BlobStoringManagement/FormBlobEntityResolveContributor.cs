using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoringManagement
{
    public class FormBlobEntityResolveContributor : HttpBlobEntityResolveContributorBase
    {
        public const string ContributorName = "From";

        public override string Name => ContributorName;

        protected override Task<BlobEntityResolveResult> GetBlobEntityFromHttpContextOrNullAsync([NotNull] IBlobEntityResolveContext context, [NotNull] HttpContext httpContext)
        {
            var result = new BlobEntityResolveResult();
            try
            {
                result.EntityType = httpContext.Request.Form["EntityType"];
                result.EntityId = httpContext.Request.Form["EntityId"];
            }
            catch (Exception ex)
            {

            }
            return Task.FromResult(result);
        }
    }
}
