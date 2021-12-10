import { NgModule } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

import { filter } from 'rxjs/operators';

import { PureThemeService } from './pure-theme.service';

@NgModule({
  declarations: [],
  imports: [],
  exports: [],
})
export class PureThemeModule {
  constructor(router: Router, pureThemeService: PureThemeService) {
    // 监听路由变更
    router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        pureThemeService.setCurrentApp(event);
      });
  }
}
