using Dignite.Abp.Identity.Localization;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.Identity.Blazor.Menus;

public class IdentityMenuContributor : IMenuContributor
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
        var administrationMenu = context.Menu.GetAdministration();

        var l = context.GetLocalizer<DigniteAbpIdentityResource>();

        var identityMenuItem= administrationMenu.FindMenuItem(IdentityMenuNames.GroupName);

        identityMenuItem.AddItem(new ApplicationMenuItem(
                DigniteAbpIdentityMenuNames.OrganizationUnits,
                l["OrganizationUnits"],
                url: "~/identity/organization-units").RequirePermissions(OrganizationUnitPermissions.OrganizationUnits.Default));

        return Task.CompletedTask;
    }
}
