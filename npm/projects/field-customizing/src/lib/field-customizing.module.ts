import { NgModule } from '@angular/core';
import { FIELD_CUSTOMIZING_PROVIDERS } from './providers/field-customizing.provider';

@NgModule({
  declarations: [],
  imports: [],
  exports: []
})
export class FieldCustomizingModule {
  static forRoot() {
    return {
      ngModule: FieldCustomizingModule,
      providers: [
        FIELD_CUSTOMIZING_PROVIDERS
      ],
    };
  }
}
