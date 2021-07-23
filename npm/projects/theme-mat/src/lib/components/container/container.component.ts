import { Component } from '@angular/core';

@Component({
    selector: 'dignite-navbar',
    template: `<ng-content></ng-content>`
})

export class NavbarComponent { }

@Component({
    selector: 'dignite-container',
    template: '<ng-content></ng-content>'
})
export class ContentComponent {

}
