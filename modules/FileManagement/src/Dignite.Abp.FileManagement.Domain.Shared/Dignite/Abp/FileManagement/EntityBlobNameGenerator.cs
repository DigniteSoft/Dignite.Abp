using Dignite.Abp.BlobStoring;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FileManagement
{
    /// <summary>
    /// </summary>
    public class EntityBlobNameGenerator : IBlobNameGenerator,ITransientDependency
    {
        private readonly IBlobEntityResolver _blobEntityResolver;

        public EntityBlobNameGenerator(IBlobEntityResolver blobEntityResolver)
        {
            _blobEntityResolver = blobEntityResolver;
        }

        public virtual async Task<string> Create()
        {
            var blobEntityResult = await _blobEntityResolver.ResolveBlobEntityAsync();

            Check.NotNullOrWhiteSpace(blobEntityResult.EntityId, nameof(blobEntityResult.EntityId), BlobConsts.MaxEntityIdLength);

            return blobEntityResult.EntityId;
        }
    }
}
