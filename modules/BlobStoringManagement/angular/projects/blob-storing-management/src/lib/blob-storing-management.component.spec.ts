import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { BlobStoringManagementComponent } from './blob-storing-management.component';

describe('BlobStoringManagementComponent', () => {
  let component: BlobStoringManagementComponent;
  let fixture: ComponentFixture<BlobStoringManagementComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ BlobStoringManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BlobStoringManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
