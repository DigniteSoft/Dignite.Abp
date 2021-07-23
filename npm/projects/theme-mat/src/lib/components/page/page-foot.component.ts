import { Component, ContentChild, Input } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';

@Component({
    selector: 'dignite-page-footer',
    templateUrl: './page-foot.component.html',
    styleUrls: ['./page-foot.component.scss']
})
export class PageFootComponent {

    @ContentChild(MatPaginator, { static: true }) paginator: MatPaginator;

    constructor(

    ) {
    }
}
