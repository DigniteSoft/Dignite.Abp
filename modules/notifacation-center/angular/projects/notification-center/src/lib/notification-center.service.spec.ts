import { TestBed } from '@angular/core/testing';

import { NotificationCenterService } from './notification-center.service';

describe('NotificationCenterService', () => {
  let service: NotificationCenterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NotificationCenterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
