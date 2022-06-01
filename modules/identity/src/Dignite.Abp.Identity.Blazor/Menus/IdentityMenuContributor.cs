using Dignite.Abp.Identity.Localization;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
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


        var identityMenuItem = new ApplicationMenuItem(IdentityMenuNames.GroupName, l["Menu:IdentityManagement"],
            icon: "far fa-id-card");
        administrationMenu.AddItem(identityMenuItem);

        identityMenuItem.AddItem(new ApplicationMenuItem(
                IdentityMenuNames.Roles,
                l["Roles"],
                icon: "fa fa-users",
                url: "~/identity/roles").RequirePermissions(IdentityPermissions.Roles.Default));

        identityMenuItem.AddItem(new ApplicationMenuItem(
            IdentityMenuNames.Users,
            l["Users"],
            icon: "fa fa-user",
            url: "~/identity/users").RequirePermissions(IdentityPermissions.Users.Default));

        identityMenuItem.AddItem(new ApplicationMenuItem(
            IdentityMenuNames.OrganizationUnits,
            l["OrganizationUnits"],
            icon: "fa fa-sitemap",
            url: "~/identity/organization-units").RequirePermissions(OrganizationUnitPermissions.OrganizationUnits.Default));


        return Task.CompletedTask;
    }
}
