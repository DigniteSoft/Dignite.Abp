import { NavItemsService } from '@abp/ng.theme.shared';
import { APP_INITIALIZER } from '@angular/core';
import { CurrentUserComponent } from '../components/nav-items/current-user.component';
import { LanguagesComponent } from '../components/nav-items/languages.component';
import { ThemesComponent } from '../components/nav-items/themes.component';
import { eThemeMatComponents } from '../enums/components';

export const BASIC_THEME_NAV_ITEM_PROVIDERS = [
  {
    provide: APP_INITIALIZER,
    useFactory: configureNavItems,
    deps: [NavItemsService],
    multi: true,
  },
];

export function configureNavItems(navItems: NavItemsService) {
  return () => {
    navItems.addItems([
      {
        id: eThemeMatComponents.Languages,
        order: 98,
        component: LanguagesComponent,
      },
      {
        id: eThemeMatComponents.Themes,
        order: 99,
        component: ThemesComponent,
      },
      {
        id: eThemeMatComponents.CurrentUser,
        order: 100,
        component: CurrentUserComponent,
      }
    ]);
  };
}
