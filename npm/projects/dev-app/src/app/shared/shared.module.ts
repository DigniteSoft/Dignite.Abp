import { CoreModule } from '@abp/ng.core';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { ThemeMatModule } from '@dignite/theme-mat';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgxValidateCoreModule } from '@ngx-validate/core';

@NgModule({
  declarations: [],
  imports: [
    CoreModule,
    ThemeSharedModule,
    ThemeMatModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
  ],
  exports: [
    CoreModule,
    ThemeSharedModule,
    ThemeMatModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
  ],
  providers: [],
})
export class SharedModule {}
