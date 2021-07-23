import { PermissionManagementModule } from '@abp/ng.permission-management';
import { ModuleWithProviders, NgModule } from '@angular/core';
import { NzBadgeModule } from 'ng-zorro-antd/badge';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzMessageModule } from 'ng-zorro-antd/message';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzTreeModule } from 'ng-zorro-antd/tree';
import { NzTreeSelectModule } from 'ng-zorro-antd/tree-select';
import { REPLACE_COMPONENTS_PROVIDER } from './providers/replaceComponents.provider';
import { CreateOrEditRolesModalComponent } from './roles/create-or-edit-roles-modal.component';
import { RolesComponent } from './roles/roles.component';
import { CoreModule } from '@abp/ng.core';

@NgModule({
  declarations: [
    RolesComponent,
    CreateOrEditRolesModalComponent
  ],
  imports: [
    CoreModule,
    NzGridModule,
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
export class BaseIdentitiyModule {

}


