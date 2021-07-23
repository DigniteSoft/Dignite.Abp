import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FieldCustomizingComponent } from './field-customizing.component';

describe('FieldCustomizingComponent', () => {
  let component: FieldCustomizingComponent;
  let fixture: ComponentFixture<FieldCustomizingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FieldCustomizingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FieldCustomizingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
