using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoringManagement
{
    public interface IBlobEntityResolveContributor
    {
        string Name { get; }

        Task ResolveAsync(IBlobEntityResolveContext context);
    }
}
