import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorPageComponent } from './doctor-page.component';

describe('DoctorPageComponent', () => {
  let component: DoctorPageComponent;
  let fixture: ComponentFixture<DoctorPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DoctorPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
