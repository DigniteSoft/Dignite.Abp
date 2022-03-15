using AntDesign;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace Dignite.Abp.Identity.Blazor.Pages.Identity
{
    public partial class OrganizationUnits
    {
        protected PageToolbar Toolbar { get; } = new();

        protected override Task OnInitializedAsync()
        {
            SetToolbarItems();

            return base.OnInitializedAsync();
        }

        protected void SetToolbarItems()
        {
            Toolbar.AddComponent<Toolbar>(null, 0, OrganizationUnitPermissions.OrganizationUnits.Create);

        }


        async Task TreeNodeClick(TreeEventArgs<OrganizationUnitDto> e)
        {
            await Task.CompletedTask;
        }
    }
}
