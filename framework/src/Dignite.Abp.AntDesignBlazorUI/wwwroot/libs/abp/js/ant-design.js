

/**
 *  Get the visualization height of the first table 
 * @param {*} extraHeight  Extra height ( The content height at the bottom of the table  Number type , The default is 74) 
 * @param {*} id  There are multiple... In the current page table We need to make table Of id
 */

window.setTableScrollY = ({ extraHeight, id }) => {
    if (typeof extraHeight == "undefined") {
        //   Default bottom pagination 55 +  Margin 10
        extraHeight = 65;

        if (document.body.clientWidth <= 576) {
            //For small screen 
            let mainNav = document.getElementById('main-navbar-nav');
            if (mainNav) {
                extraHeight = extraHeight + mainNav.clientHeight;
            }
        }
    }
    let tBody = null
    if (id) {
        tBody = document.getElementById(id) ? document.getElementById(id).getElementsByClassName("ant-table-body")[0] : null
    } else {
        tBody = document.getElementsByClassName("ant-table-body")[0]
    }
    // The distance from the table content to the top 
    let tBodyTop = 0
    if (tBody) {
        tBodyTop = tBody.getBoundingClientRect().top
    }
    else {
        console.warn("ant-table-body is not found.Set the initial value of scrolly in the ant table component.");
        return;
    }

    // Form height - The height of the top of the table content - The height of the bottom of the table content 
    let height = `calc(100vh - ${tBodyTop + extraHeight}px)`
    tBody.setAttribute('style', 'overflow-y: scroll; max-height: ' + height);
}