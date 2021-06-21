import { TestBed } from '@angular/core/testing';

import { BlobStoringManagementService } from './blob-storing-management.service';

describe('BlobStoringManagementService', () => {
  let service: BlobStoringManagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BlobStoringManagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
