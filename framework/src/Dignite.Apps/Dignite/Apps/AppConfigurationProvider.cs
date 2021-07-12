using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dignite.Apps
{
    public class AppConfigurationProvider : IAppConfigurationProvider
    {
        protected virtual IAppStore AppStore { get; }

        public async Task<IReadOnlyList<AppConfiguration>> GetListAsync()
        {
            return await AppStore.GetListAsync();
        }

        public async Task<AppConfiguration> GetAsync(string appId)
        {
            return await AppStore.FindAsync(appId);
        }
    }
}
