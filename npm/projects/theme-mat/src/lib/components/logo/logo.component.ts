import { ApplicationInfo, EnvironmentService } from '@abp/ng.core';
import { Component } from '@angular/core';

@Component({
  selector: 'dignite-logo',
  template: `
    <a routerLink="/">
      <img
        *ngIf="appInfo.logoUrl; else appName"
        [src]="appInfo.logoUrl"
        [alt]="appInfo.name"
        height="auto"
      />
    </a>

    <ng-template #appName>
      {{ appInfo.name }}
    </ng-template>
  `,
})
export class LogoComponent {
  get appInfo(): ApplicationInfo {
    return this.environment.getEnvironment().application;
  }

  constructor(private environment: EnvironmentService) { }
}
