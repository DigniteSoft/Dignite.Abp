import { CoreModule } from '@abp/ng.core';
import { PermissionManagementModule } from '@abp/ng.permission-management';
import { ModuleWithProviders, NgModule } from '@angular/core';
import { ThemeMatModule } from '@dignite/theme-mat';
import { NzBadgeModule } from 'ng-zorro-antd/badge';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzMessageModule } from 'ng-zorro-antd/message';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzTreeModule } from 'ng-zorro-antd/tree';
import { NzTreeSelectModule } from 'ng-zorro-antd/tree-select';
import { IDENTITY_REPLACE_COMPONENTS_PROVIDER } from './providers/replace-components.provider';
import { CreateOrEditRolesModalComponent } from './roles/create-or-edit-roles-modal.component';
import { RolesComponent } from './roles/roles.component';

@NgModule({
  declarations: [
    RolesComponent,
    CreateOrEditRolesModalComponent
  ],
  imports: [
    CoreModule,
    ThemeMatModule,
    NzButtonModule,
    NzCardModule,
    NzTableModule,
    NzModalModule,
    NzMessageModule,
    NzBadgeModule,
    NzFormModule,
    NzInputModule,
    NzTreeModule,
    NzTreeSelectModule,
    NzPaginationModule,
    NzSelectModule,
    NzSwitchModule,
    NzDividerModule,
    PermissionManagementModule
  ],
  exports: [
    RolesComponent,
    CreateOrEditRolesModalComponent
  ]
})
export class IdentitiyModule {
  static forRoot(): ModuleWithProviders<IdentitiyModule> {
    return {
      ngModule: IdentitiyModule,
      providers: [IDENTITY_REPLACE_COMPONENTS_PROVIDER]
    };
  }
}


