import { Component, Input } from '@angular/core';

@Component({
    selector: 'dignite-page-table',
    templateUrl: './page-table.component.html'
})
export class PageTableComponent {
    @Input() isLoading = true;
    @Input() totalCount: number;
}
