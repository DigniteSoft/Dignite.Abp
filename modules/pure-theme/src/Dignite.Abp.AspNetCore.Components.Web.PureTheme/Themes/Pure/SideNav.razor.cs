using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
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

        /// <summary>
        /// 侧边栏导航的根菜单
        /// </summary>
        private ApplicationMenuItem RootMenuItem { get; set; }



        protected override async Task OnInitializedAsync()
        {
            //
            NavigationManager.LocationChanged += OnLocationChanged;

            //根据当前页url查询根菜单
            FindRootMenuItemAsync(NavigationManager.Uri);


            await base.OnInitializedAsync();

        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= OnLocationChanged;
        }



        private void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            /*
            if (RootMenuItem != null && RootMenuItem.Items != null)
            {
                var location = e.Location.Replace(NavigationManager.BaseUri, "");
                //如果当前的导航菜单包含当前URL，则不需要重新
                if (RootMenuItem.Items.Any(i => i != null && i.Url != null && location.StartsWith(i.Url.TrimStart('/', '~'), StringComparison.OrdinalIgnoreCase)))
                {
                    return;
                }
            }
            */

            //根据新页面url查询根菜单
            FindRootMenuItemAsync(e.Location);

            InvokeAsync(StateHasChanged);
        }

        private async void FindRootMenuItemAsync(string location)
        {
            location = location.Replace(NavigationManager.BaseUri, "");
            var mainMenu = await MenuManager.GetMainMenuAsync();
            RootMenuItem = mainMenu.Items.FirstOrDefault(menu =>
                menu.Url!=null && !menu.Url.TrimStart('/', '~').IsNullOrEmpty() && location.StartsWith(menu.Url.TrimStart('/', '~'), StringComparison.OrdinalIgnoreCase)
                );
            if (RootMenuItem == null)
            {
                foreach (var topMenuItem in mainMenu.Items)
                {
                    FindRootMenuItemWithChildren(topMenuItem, topMenuItem.Items, location);
                }
            }
        }

        private void FindRootMenuItemWithChildren(ApplicationMenuItem topMenuItem, ApplicationMenuItemList menuItems, string location)
        {
            var menu = menuItems.FirstOrDefault(menu => menu.Url != null && location.StartsWith(menu.Url.TrimStart('/', '~'), StringComparison.OrdinalIgnoreCase));
            if (menu != null)
            {
                RootMenuItem = topMenuItem;
                return;
            }
            if (RootMenuItem == null)
            {
                foreach (var menuItem in menuItems)
                {
                    FindRootMenuItemWithChildren(topMenuItem, menuItem.Items, location);
                }
            }
        }
    }
}
