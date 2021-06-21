import { ModuleWithProviders, NgModule } from '@angular/core';
import { BLOB_STORING_MANAGEMENT_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class BlobStoringManagementConfigModule {
  static forRoot(): ModuleWithProviders<BlobStoringManagementConfigModule> {
    return {
      ngModule: BlobStoringManagementConfigModule,
      providers: [BLOB_STORING_MANAGEMENT_ROUTE_PROVIDERS],
    };
  }
}
