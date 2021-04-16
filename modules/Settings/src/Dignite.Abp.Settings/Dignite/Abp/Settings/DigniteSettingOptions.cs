namespace Dignite.Abp.Settings
{
    public class DigniteSettingOptions
    {
        public SettingNavigations Navigations { get; }

        public DigniteSettingOptions()
        {
            Navigations = new SettingNavigations();
        }
    }
}
