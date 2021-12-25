import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NotificationCenterComponent } from './components/notification-center.component';
import { NotificationCenterRoutingModule } from './notification-center-routing.module';

@NgModule({
  declarations: [NotificationCenterComponent],
  imports: [CoreModule, ThemeSharedModule, NotificationCenterRoutingModule],
  exports: [NotificationCenterComponent],
})
export class NotificationCenterModule {
  static forChild(): ModuleWithProviders<NotificationCenterModule> {
    return {
      ngModule: NotificationCenterModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<NotificationCenterModule> {
    return new LazyModuleFactory(NotificationCenterModule.forChild());
  }
}
