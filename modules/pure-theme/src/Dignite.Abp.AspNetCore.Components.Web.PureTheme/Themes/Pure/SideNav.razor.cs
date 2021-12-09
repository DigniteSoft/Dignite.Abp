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
        private IJSRuntime JS { get; set; }

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
            FindRootMenuItem(NavigationManager.Uri);

            //调用JS方法，设置是否显示侧边栏
            await JS.InvokeVoidAsync("mainMenuToggle", (RootMenuItem == null || RootMenuItem.IsLeaf) ? false : true);

            await base.OnInitializedAsync();

        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= OnLocationChanged;
        }



        private void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            if (RootMenuItem != null && RootMenuItem.Items != null)
            {
                var location = NavigationManager.Uri.Replace(NavigationManager.BaseUri, "");
                //如果当前的导航菜单包含当前URL，则不需要重新
                if (RootMenuItem.Items.Any(i => i != null && i.Url != null && location.StartsWith(i.Url.TrimStart('/', '~'), StringComparison.OrdinalIgnoreCase)))
                {
                    return;
                }
            }

            //根据新页面url查询根菜单
            FindRootMenuItem(e.Location);

            //调用JS方法，设置是否显示侧边栏
            JS.InvokeVoidAsync("mainMenuToggle", (RootMenuItem == null || RootMenuItem.IsLeaf) ? false : true);

            InvokeAsync(StateHasChanged);
        }

        private void FindRootMenuItem(string location)
        {
            location = NavigationManager.Uri.Replace(NavigationManager.BaseUri, "");

            var mainMenu = (MenuManager.GetMainMenuAsync()).Result;
            RootMenuItem = mainMenu.Items?.FirstOrDefault(i => i != null && i.Url != null && location.StartsWith(i.Url.TrimStart('/', '~'), StringComparison.OrdinalIgnoreCase));
            if (RootMenuItem == null)
            {
                foreach (var topMenuItem in mainMenu.Items)
                {
                    if (RootMenuItem == null)
                    {
                        FindRootMenuItemFromChildren(topMenuItem, topMenuItem.Items, location);
                    }
                }
            }

        }

        private void FindRootMenuItemFromChildren(ApplicationMenuItem topMenuItem, ApplicationMenuItemList menuItems, string location)
        {
            var menu = menuItems?.FirstOrDefault(i => i != null && i.Url != null && location.StartsWith(i.Url.TrimStart('/', '~'), StringComparison.OrdinalIgnoreCase));
            if (menu != null)
            {
                RootMenuItem = topMenuItem;
                return;
            }
            if (RootMenuItem == null)
            {
                foreach (var menuItem in menuItems)
                {
                    FindRootMenuItemFromChildren(topMenuItem, menuItem.Items, location);
                }
            }
        }
    }
}
