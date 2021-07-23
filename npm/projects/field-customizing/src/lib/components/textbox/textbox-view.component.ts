import { Component } from '@angular/core';
import { FieldCustomizing } from '../../models/field-customizing';

@Component({
    selector: 'dignite-textbox',
    templateUrl: './textbox-view.component.html'
})
export class TextBoxViewComponent extends FieldCustomizing.FormProviderComponent<any> {

}
