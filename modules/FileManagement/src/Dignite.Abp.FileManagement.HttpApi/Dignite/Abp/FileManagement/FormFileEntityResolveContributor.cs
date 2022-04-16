using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Dignite.Abp.FileManagement
{
    public class FormFileEntityResolveContributor : HttpFileEntityResolveContributorBase
    {
        public const string ContributorName = "Form";

        public override string Name => ContributorName;

        protected override async Task<FileEntityResolveResult> GetBlobEntityFromHttpContextOrNullAsync([NotNull] IFileEntityResolveContext context, [NotNull] HttpContext httpContext)
        {
            if (!httpContext.Request.HasFormContentType)
            {
                return null;
            }


            var result = new FileEntityResolveResult();
            var form = await httpContext.Request.ReadFormAsync();

            if (form.ContainsKey(FileEntityResolverConsts.EntityTypeKey)
                && form.ContainsKey(FileEntityResolverConsts.EntityIdKey))
            {
                result.EntityType = form[FileEntityResolverConsts.EntityTypeKey];
                result.EntityId = form[FileEntityResolverConsts.EntityIdKey];
            }

            return result;
        }
    }
}
