import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { ModuleWithProviders, NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { NavigationEnd, Router } from '@angular/router';
import {
  NgxValidateCoreModule,
  VALIDATION_ERROR_TEMPLATE,
  VALIDATION_INVALID_CLASSES,
  VALIDATION_TARGET_SELECTOR
} from '@ngx-validate/core';
import { NgxsModule } from '@ngxs/store';
import { filter } from 'rxjs/operators';
import { ValidationErrorComponent } from './components';
import { AccountLayoutComponent } from './components/account-layout.component';
import { ApplicationLayoutComponent } from './components/application-layout.component';
import { AppsComponent } from './components/apps/apps.component';
import { ContentComponent, NavbarComponent } from './components/container/container.component';
import { PageAlertContainerComponent } from './components/container/page-alert-container/page-alert-container.component';
import { EmptyLayoutComponent } from './components/empty-layout.component';
import { LogoComponent } from './components/logo/logo.component';
import { CurrentUserComponent } from './components/nav-items/current-user.component';
import { LanguagesComponent } from './components/nav-items/languages.component';
import { NavItemComponent } from './components/nav-items/nav-item.component';
import { NavItemsComponent } from './components/nav-items/nav-items.component';
import { ThemesComponent } from './components/nav-items/themes.component';
import { PageContentComponent } from './components/page/page-content.component';
import { PageFootComponent } from './components/page/page-foot.component';
import { PageHeaderComponent } from './components/page/page-header.component';
import { PageSidebarComponent } from './components/page/page-sidebar.component';
import { PageTableComponent } from './components/page/page-table.component';
import { PageTopToolsComponent } from './components/page/page-top-tools.component';
import { PageComponent } from './components/page/page.component';
import { RoutesComponent } from './components/routes/routes.component';
import { ThemeMatOptions } from './models/theme';
import { THEME_MAT_NAV_ITEM_PROVIDERS, THEME_MAT_OPTIONS, THEME_MAT_STYLES_PROVIDERS, THEME_MAT_THEMES_PROVIDERS } from './providers';
import { AppService } from './services/app.service';
import { AppState } from './states/app.state';

export const LAYOUTS = [ApplicationLayoutComponent, AccountLayoutComponent, EmptyLayoutComponent];


@NgModule({
  imports: [
    CoreModule,
    ThemeSharedModule,
    NgxsModule.forFeature([AppState]),
    MatSidenavModule,
    MatListModule,
    MatToolbarModule,
    MatButtonModule,
    MatMenuModule,
    MatTooltipModule,
    NgxValidateCoreModule,
  ],
  declarations: [
    ...LAYOUTS,
    NavbarComponent,
    ContentComponent,
    RoutesComponent,
    LogoComponent,
    AppsComponent,
    NavItemsComponent,
    NavItemComponent,
    CurrentUserComponent,
    LanguagesComponent,
    ThemesComponent,
    ValidationErrorComponent,
    PageAlertContainerComponent,
    PageComponent,
    PageHeaderComponent,
    PageSidebarComponent,
    PageContentComponent,
    PageTableComponent,
    PageTopToolsComponent,
    PageFootComponent,
  ],
  exports: [
    ...LAYOUTS,
    NavbarComponent,
    ContentComponent,
    RoutesComponent,
    CurrentUserComponent,
    LanguagesComponent,
    LogoComponent,
    ThemesComponent,
    AppsComponent,
    NavItemsComponent,
    NavItemComponent,
    ValidationErrorComponent,
    PageComponent,
    PageAlertContainerComponent,
    PageHeaderComponent,
    PageSidebarComponent,
    PageContentComponent,
    PageTableComponent,
    PageTopToolsComponent,
    PageFootComponent,
  ]
})
export class BaseThemeMatModule { }

@NgModule({
  exports: [BaseThemeMatModule],
  imports: [BaseThemeMatModule],
})
export class ThemeMatModule {
  constructor(
    router: Router,
    appService: AppService,
  ) {
    // 监听路由变更
    router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        appService.setCurrentApp(event);
      });
  }

  static forRoot(options = {} as ThemeMatOptions): ModuleWithProviders<ThemeMatModule> {
    return {
      ngModule: ThemeMatModule,
      providers: [
        THEME_MAT_NAV_ITEM_PROVIDERS,
        THEME_MAT_STYLES_PROVIDERS,
        { provide: THEME_MAT_OPTIONS, useValue: options },
        THEME_MAT_THEMES_PROVIDERS,
        {
          provide: VALIDATION_ERROR_TEMPLATE,
          useValue: ValidationErrorComponent,
        },
        {
          provide: VALIDATION_TARGET_SELECTOR,
          useValue: '.form-group',
        },
        {
          provide: VALIDATION_INVALID_CLASSES,
          useValue: 'is-invalid',
        },
      ],
    };
  }
}

