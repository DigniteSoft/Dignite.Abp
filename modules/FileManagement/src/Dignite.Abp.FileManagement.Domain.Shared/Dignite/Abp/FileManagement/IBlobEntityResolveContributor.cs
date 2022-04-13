using System.Threading.Tasks;

namespace Dignite.Abp.FileManagement
{
    public interface IBlobEntityResolveContributor
    {
        string Name { get; }

        Task ResolveAsync(IBlobEntityResolveContext context);
    }
}
