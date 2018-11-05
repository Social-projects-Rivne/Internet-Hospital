import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddDoctorPlansComponent } from './add-doctor-plans.component';

describe('AddDoctorPlansComponent', () => {
  let component: AddDoctorPlansComponent;
  let fixture: ComponentFixture<AddDoctorPlansComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddDoctorPlansComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddDoctorPlansComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
