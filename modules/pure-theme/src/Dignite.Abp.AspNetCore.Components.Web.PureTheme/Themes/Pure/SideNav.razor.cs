using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Components.Web.PureTheme.Themes.Pure
{
    public partial class SideNav
    {
        [Inject]
        protected IMenuManager MenuManager { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }

        private ApplicationMenuItem TopMenuItem { get; set; }


        protected override async Task OnInitializedAsync()
        {
            NavigationManager.LocationChanged += OnLocationChanged;

            await base.OnInitializedAsync();

            FindCurrentMenuItem(NavigationManager.Uri);
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= OnLocationChanged;
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            if (TopMenuItem != null && TopMenuItem.Items != null)
            {
                var location = NavigationManager.Uri.Replace(NavigationManager.BaseUri, "");
                //如果当前的导航菜单包含当前URL，则不需要重新
                if (TopMenuItem.Items.Any(i => i != null && i.Url != null && location.StartsWith(i.Url.TrimStart('/', '~'), StringComparison.OrdinalIgnoreCase)))
                {
                    return;
                }
            }

            FindCurrentMenuItem(e.Location);

            InvokeAsync(StateHasChanged);
        }

        private void FindTopMenuItemFromChildren(ApplicationMenuItem topMenuItem, ApplicationMenuItemList menuItems, string location)
        {
            var menu = menuItems?.FirstOrDefault(i => i != null && i.Url != null && location.StartsWith(i.Url.TrimStart('/', '~'), StringComparison.OrdinalIgnoreCase));
            if (menu != null)
            {
                TopMenuItem = topMenuItem;
                return;
            }
            if (TopMenuItem == null)
            {
                foreach (var menuItem in menuItems)
                {
                    FindTopMenuItemFromChildren(topMenuItem, menuItem.Items, location);
                }
            }
        }

        private void FindCurrentMenuItem(string location)
        {
            location = NavigationManager.Uri.Replace(NavigationManager.BaseUri, "");

            var mainMenu = (MenuManager.GetMainMenuAsync()).Result;
            TopMenuItem = mainMenu.Items?.FirstOrDefault(i => i != null && i.Url != null && location.StartsWith(i.Url.TrimStart('/', '~'), StringComparison.OrdinalIgnoreCase));
            if (TopMenuItem == null)
            {
                foreach (var topMenuItem in mainMenu.Items)
                {
                    if (TopMenuItem == null)
                    {
                        FindTopMenuItemFromChildren(topMenuItem, topMenuItem.Items, location);
                    }
                }
            }
        }
    }
}
