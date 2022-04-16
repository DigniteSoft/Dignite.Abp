
using System.Threading.Tasks;

namespace Dignite.Abp.FileManagement
{
    public abstract class FileEntityResolveContributorBase:IFileEntityResolveContributor
    {
        public abstract string Name { get; }

        public abstract Task ResolveAsync(IFileEntityResolveContext context);
    }
}
