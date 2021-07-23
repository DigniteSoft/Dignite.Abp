import { TestBed } from '@angular/core/testing';

import { FieldCustomizingService } from './field-customizing.service';

describe('FieldCustomizingService', () => {
  let service: FieldCustomizingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FieldCustomizingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
