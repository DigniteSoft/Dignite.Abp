using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.BlobStoringManagement.Blazor.Menus
{
    public class BlobStoringManagementMenuContributor : IMenuContributor
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
            context.Menu.AddItem(new ApplicationMenuItem(BlobStoringManagementMenus.Prefix, displayName: "BlobStoringManagement", "/BlobStoringManagement", icon: "fa fa-globe"));
            
            return Task.CompletedTask;
        }
    }
}