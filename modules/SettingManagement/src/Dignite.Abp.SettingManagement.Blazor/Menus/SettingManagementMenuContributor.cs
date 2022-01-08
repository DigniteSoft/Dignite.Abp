using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.SettingManagement.Blazor.Menus
{
    public class SettingManagementMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            //Add main menu items.
            context.Menu.AddItem(new ApplicationMenuItem(
                SettingManagementMenus.Prefix, 
                displayName: "SettingManagement", 
                "/SettingManagement/global", 
                icon: "fa fa-cog"));
            
            return Task.CompletedTask;
        }
    }
}