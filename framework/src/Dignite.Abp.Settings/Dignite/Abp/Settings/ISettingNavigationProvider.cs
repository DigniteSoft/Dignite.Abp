
namespace Dignite.Abp.Settings
{
    /// <summary>
    /// Define setting navigation provider
    /// </summary>
    public interface ISettingNavigationProvider
    {
        SettingNavigation Navigation { get; }
    }
}
