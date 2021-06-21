import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { BlobStoringManagementComponent } from './components/blob-storing-management.component';
import { BlobStoringManagementRoutingModule } from './blob-storing-management-routing.module';

@NgModule({
  declarations: [BlobStoringManagementComponent],
  imports: [CoreModule, ThemeSharedModule, BlobStoringManagementRoutingModule],
  exports: [BlobStoringManagementComponent],
})
export class BlobStoringManagementModule {
  static forChild(): ModuleWithProviders<BlobStoringManagementModule> {
    return {
      ngModule: BlobStoringManagementModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<BlobStoringManagementModule> {
    return new LazyModuleFactory(BlobStoringManagementModule.forChild());
  }
}
