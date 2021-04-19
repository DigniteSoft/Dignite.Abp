using System.Collections.Generic;

namespace Dignite.Abp.SettingManagement
{
    public class UpdateSettingsInput
    {
        public string NavigationName { get; set; }

        public List<SettingEditInput> Settings { get; set; }
    }
}
