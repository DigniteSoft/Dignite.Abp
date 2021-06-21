
using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoringManagement
{
    public abstract class BlobEntityResolveContributorBase:IBlobEntityResolveContributor
    {
        public abstract string Name { get; }

        public abstract Task ResolveAsync(IBlobEntityResolveContext context);
    }
}
