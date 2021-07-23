import {
  AuthService,
  ConfigStateService,
  CurrentUserDto,
  NAVIGATE_TO_MANAGE_PROFILE,
  SessionStateService,
} from '@abp/ng.core';
import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'dignite-current-user',
  templateUrl: './current-user.component.html',
  styleUrls: ['./current-user.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class CurrentUserComponent {
  currentUser$: Observable<CurrentUserDto> = this.configState.getOne$('currentUser');
  selectedTenant$ = this.sessionState.getTenant$();

  constructor(
    @Inject(NAVIGATE_TO_MANAGE_PROFILE) public navigateToManageProfile,
    private authService: AuthService,
    private configState: ConfigStateService,
    private sessionState: SessionStateService,
  ) { }

  navigateToLogin() {
    this.authService.navigateToLogin();
  }

  logout() {
    this.authService.logout().subscribe();
  }
}
