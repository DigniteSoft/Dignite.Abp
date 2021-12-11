import { TestBed } from '@angular/core/testing';

import { PureThemeService } from './pure-theme.service';

describe('PureThemeService', () => {
  let service: PureThemeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PureThemeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
