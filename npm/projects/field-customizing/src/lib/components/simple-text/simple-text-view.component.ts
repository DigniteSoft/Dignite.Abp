import { Component } from '@angular/core';
import { FieldCustomizing } from '../../models/field-customizing';

@Component({
    selector: 'dignite-simple-text-view',
    templateUrl: './simple-text-view.component.html'
})
export class SimpleTextViewComponent extends FieldCustomizing.FormProviderComponent<any> {

}
