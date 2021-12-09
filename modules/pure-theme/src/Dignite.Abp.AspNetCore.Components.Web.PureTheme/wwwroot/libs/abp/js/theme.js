
//主菜单的切换
window.mainMenuToggle = (isHaveChildMenus) => {
    if (isHaveChildMenus === false) {
        document.body.classList.add("no-child-menu");
    }
    else {
        document.body.classList.remove("no-child-menu");
    }
};

//左侧菜单显示切换
window.sideNavToggle = () => {
    var sideNavbar = document.getElementById('sideNavbar');
    sideNavbar.classList.toggle("show");
};