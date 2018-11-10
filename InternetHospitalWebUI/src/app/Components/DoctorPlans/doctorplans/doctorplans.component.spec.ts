import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorplansComponent } from './doctorplans.component';

describe('DoctorplansComponent', () => {
  let component: DoctorplansComponent;
  let fixture: ComponentFixture<DoctorplansComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DoctorplansComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorplansComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
