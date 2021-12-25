import { ModuleWithProviders, NgModule } from '@angular/core';
import { NOTIFICATION_CENTER_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class NotificationCenterConfigModule {
  static forRoot(): ModuleWithProviders<NotificationCenterConfigModule> {
    return {
      ngModule: NotificationCenterConfigModule,
      providers: [NOTIFICATION_CENTER_ROUTE_PROVIDERS],
    };
  }
}
