
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dignite.Apps
{
    public interface IAppStore
    {
        Task<IReadOnlyList<AppConfiguration>> GetListAsync();

        Task<AppConfiguration> FindAsync(string id);
    }
}
