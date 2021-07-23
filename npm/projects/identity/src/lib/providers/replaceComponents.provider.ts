import { ReplaceableComponentsService } from '@abp/ng.core';
import { eIdentityComponents } from '@abp/ng.identity';
import { APP_INITIALIZER } from '@angular/core';
import { RolesComponent } from '../roles/roles.component';

export const REPLACE_COMPONENTS_PROVIDER = [
  {
    provide: APP_INITIALIZER,
    useFactory: configureRoutes,
    deps: [ReplaceableComponentsService],
    multi: true,
  },
];

export function configureRoutes(replaceableComponents: ReplaceableComponentsService) {
  return () => {
    replaceableComponents.add({
      component: RolesComponent,
      key: eIdentityComponents.Roles,
    });
  };
}
