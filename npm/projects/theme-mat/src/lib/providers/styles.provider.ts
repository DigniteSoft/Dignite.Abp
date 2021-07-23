import { CONTENT_STRATEGY, DomInsertionService, ReplaceableComponentsService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { AccountLayoutComponent } from '../components/account-layout.component';
import { ApplicationLayoutComponent } from '../components/application-layout.component';
import { EmptyLayoutComponent } from '../components/empty-layout.component';
import styles from '../constants/styles';
import { eThemeMatComponents } from '../enums/components';

export const BASIC_THEME_STYLES_PROVIDERS = [
  {
    provide: APP_INITIALIZER,
    useFactory: configureStyles,
    deps: [DomInsertionService, ReplaceableComponentsService],
    multi: true,
  },
];

export function configureStyles(
  domInsertion: DomInsertionService,
  replaceableComponents: ReplaceableComponentsService,
) {
  return () => {
    domInsertion.insertContent(CONTENT_STRATEGY.AppendStyleToHead(styles));

    initLayouts(replaceableComponents);
  };
}

function initLayouts(replaceableComponents: ReplaceableComponentsService) {
  replaceableComponents.add({
    key: eThemeMatComponents.ApplicationLayout,
    component: ApplicationLayoutComponent,
  });
  replaceableComponents.add({
    key: eThemeMatComponents.AccountLayout,
    component: AccountLayoutComponent,
  });
  replaceableComponents.add({
    key: eThemeMatComponents.EmptyLayout,
    component: EmptyLayoutComponent,
  });
}
