using System.Threading.Tasks;

namespace Dignite.Abp.FileManagement
{
    public interface IFileEntityResolveContributor
    {
        string Name { get; }

        Task ResolveAsync(IFileEntityResolveContext context);
    }
}
