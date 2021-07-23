import { eLayoutType, SubscriptionService } from '@abp/ng.core';
import { collapseWithMargin, slideFromBottom } from '@abp/ng.theme.shared';
import { AfterViewInit, Component, ViewEncapsulation } from '@angular/core';
import { DigniteThemeService } from '../services/theme.service';

@Component({
  selector: 'dignite-application',
  templateUrl: './application-layout.component.html',
  styleUrls: ['./application-layout.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: [slideFromBottom, collapseWithMargin],
  providers: [SubscriptionService],
})
export class ApplicationLayoutComponent implements AfterViewInit {
  // required for dynamic component
  static type = eLayoutType.application;

  constructor(private themeService: DigniteThemeService) { }

  ngAfterViewInit() {
  }
}
