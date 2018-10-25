import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorListItemComponent } from './doctor-list-item.component';

describe('DoctorListItemComponent', () => {
  let component: DoctorListItemComponent;
  let fixture: ComponentFixture<DoctorListItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DoctorListItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
