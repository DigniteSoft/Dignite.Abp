import { NavItem, NavItemsService } from '@abp/ng.theme.shared';
import { Component, TrackByFunction } from '@angular/core';

@Component({
  selector: 'dignite-nav-items',
  templateUrl: './nav-items.component.html',
})
export class NavItemsComponent {
  trackByFn: TrackByFunction<NavItem> = (_, element) => element.id;

  constructor(public readonly navItems: NavItemsService) { }
}
