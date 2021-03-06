using AntDesign;
using Dignite.Abp.Identity.Localization;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Blazorise;

namespace Dignite.Abp.Identity.Blazor.Pages.Identity
{
    public partial class OrganizationUnitManagement
    {
        protected PageToolbar Toolbar { get; } = new();
        OrganizationUnitDto selectedOrganizationUnit = null;

        OrganizationUnitMembersComponent MembersComponent;


        public OrganizationUnitManagement()
        {
            LocalizationResource = typeof(DigniteAbpIdentityResource);

        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected ValueTask SetToolbarItemsAsync()
        {
            Toolbar.Contributors.Clear();
            Toolbar.AddButton(L["AddMembers"],
                OpenCreateModalAsync,
                IconName.Add,
                requiredPolicyName: OrganizationUnitPermissions.OrganizationUnits.Update);

            return ValueTask.CompletedTask;
        }

        async Task TreeNodeClick(TreeEventArgs<OrganizationUnitDto> e)
        {
            await SetToolbarItemsAsync();
            selectedOrganizationUnit = e.Node.DataItem;
        }

        private async Task OpenCreateModalAsync()
        { 
            await MembersComponent.OpenCreateModalAsync();
        }
    }
}
