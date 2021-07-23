import { Component, ContentChild, HostBinding } from '@angular/core';
import { PageSidebarComponent } from './page-sidebar.component';

@Component({
    selector: 'dignite-page-content',
    templateUrl: './page-content.component.html',
    styles: [
        `flex:{display:flex;height:100%}`
    ]
})
export class PageContentComponent {
    @ContentChild(PageSidebarComponent, { static: true }) pageSidebar: PageSidebarComponent;

    @HostBinding('class.flex')
    get pageSidebarComponent() {
        return this.pageSidebar;
    }
}
