using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoringManagement
{
    public class BodyStringBlobEntityResolveContributor : HttpBlobEntityResolveContributorBase
    {
        public const string ContributorName = "QueryString";

        public override string Name => ContributorName;

        protected override Task<BlobEntityResolveResult> GetBlobEntityFromHttpContextOrNullAsync([NotNull] IBlobEntityResolveContext context, [NotNull] HttpContext httpContext)
        {
            var result = new BlobEntityResolveResult();
            try
            {
                //StreamReader stream = new StreamReader(httpContext.Request.Body);
                //string body = stream.ReadToEnd();
                //httpContext.Request.EnableBuffering();
                using (var reader = new StreamReader(httpContext.Request.Body, System.Text.Encoding.UTF8))
                {
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);  //大概是== Request.Body.Position = 0;的意思
                    var readerStr = reader.ReadToEndAsync().Result;
                    var input = Newtonsoft.Json.JsonConvert.DeserializeObject<SaveStreamInput>(readerStr);
                    result.EntityType = input?.EntityType;
                    result.EntityId = input?.EntityId;
                }

            }
            catch (Exception ex)
            {

            }
            return Task.FromResult(result);
        }
    }
}
