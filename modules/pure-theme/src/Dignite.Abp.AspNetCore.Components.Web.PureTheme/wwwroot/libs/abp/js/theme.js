
//主菜单的切换
window.mainMenuToggle = (isHaveChildMenus) => {
    if (isHaveChildMenus === false) {
        document.body.classList.add("no-side-navbar");
    }
    else {
        document.body.classList.remove("no-side-navbar");
    }
};

//左侧菜单显示切换
window.sideNavToggle = () => {
    var sideNavbar = document.getElementById('sideNavbar');
    sideNavbar.classList.toggle("show");
};