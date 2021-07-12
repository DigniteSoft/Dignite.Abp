using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dignite.Apps
{
    public interface IAppConfigurationProvider
    {
        Task<IReadOnlyList<AppConfiguration>> GetListAsync();

        Task<AppConfiguration> GetAsync(string appId);
    }
}
