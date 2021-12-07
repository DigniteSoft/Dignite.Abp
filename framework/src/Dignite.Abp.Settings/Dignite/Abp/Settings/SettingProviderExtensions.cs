
using Dignite.Abp.FieldCustomizing.FieldControls.DataDictionary;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings
{
    public static class SettingProviderExtensions
    {
        public static async Task<List<DataDictionary>> GetDataDictionariesAsync([NotNull] this ISettingProvider settingProvider, [NotNull] string name)
        {
            Check.NotNull(settingProvider, nameof(settingProvider));
            Check.NotNull(name, nameof(name));

            var dataDictionaries = JsonSerializer.Deserialize<List<DataDictionary>>(
                await settingProvider.GetOrNullAsync(name)
                );

            return dataDictionaries;
        }
    }
}
