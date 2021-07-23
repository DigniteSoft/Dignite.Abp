import { AccountConfigModule } from '@abp/ng.account/config';
import { CoreModule } from '@abp/ng.core';
import { registerLocale } from '@abp/ng.core/locale';
import { IdentityConfigModule } from '@abp/ng.identity/config';
import { SettingManagementConfigModule } from '@abp/ng.setting-management/config';
import { TenantManagementConfigModule } from '@abp/ng.tenant-management/config';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxsModule } from '@ngxs/store';
import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { APP_ROUTE_PROVIDER } from './route.provider';
import { ThemeMatModule, ThemeMatTheme } from '@dignite/theme-mat';
import { IdentitiyModule } from '@dignite/identity';

const themes: ThemeMatTheme[] = [{
  displayName: 'Angular Material Theme1',
  name: 'deeppurple-amber',
  primaryColor: 'deeppurple',
  type: 'light',
  styleHref: '/assets/themes/deeppurple-amber.css'
}, {
  displayName: 'Angular Material Theme2',
  name: 'indigo-pink',
  primaryColor: 'indigo',
  type: 'light',
  styleHref: '/assets/themes/indigo-pink.css',
}, {
  displayName: 'Angular Material Theme3',
  name: 'pink-bluegrey',
  primaryColor: 'pink',
  type: 'dark',
  styleHref: '/assets/themes/pink-bluegrey.css'
}, {
  displayName: 'Angular Material Theme4',
  name: 'purple-green',
  primaryColor: 'purple',
  type: 'dark',
  styleHref: '/assets/themes/purple-green.css'
}];

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CoreModule.forRoot({
      environment,
      registerLocaleFn: registerLocale(),
      sendNullsAsQueryParam: false,
      skipGetAppConfiguration: false,
    }),
    ThemeSharedModule.forRoot(),
    AccountConfigModule.forRoot(),
    IdentityConfigModule.forRoot(),
    TenantManagementConfigModule.forRoot(),
    SettingManagementConfigModule.forRoot(),
    IdentitiyModule.forRoot(),
    ThemeMatModule.forRoot({ themes }),
    NgxsModule.forRoot(),
  ],
  providers: [APP_ROUTE_PROVIDER],
  declarations: [AppComponent],
  bootstrap: [AppComponent],
})
export class AppModule { }
