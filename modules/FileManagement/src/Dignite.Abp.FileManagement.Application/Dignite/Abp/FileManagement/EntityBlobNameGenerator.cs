using Dignite.Abp.BlobStoring;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FileManagement
{
    /// <summary>
    /// </summary>
    public class EntityIdBlobNameGenerator : IBlobNameGenerator,ITransientDependency
    {
        private readonly ICurrentBlobInfo _currentFile;

        public EntityIdBlobNameGenerator(ICurrentBlobInfo currentFile)
        {
            _currentFile = currentFile;
        }

        public virtual Task<string> Create(string extensionName = null)
        {
            var file = (File)_currentFile.BlobInfo;
            Check.NotNullOrWhiteSpace(file.EntityId, nameof(file.EntityId), FileConsts.MaxEntityIdLength);

            return Task.FromResult(file.EntityId);
        }
    }
}
