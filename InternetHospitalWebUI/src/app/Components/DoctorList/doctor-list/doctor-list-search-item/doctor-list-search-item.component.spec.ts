import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorListSearchItemComponent } from './doctor-list-search-item.component';

describe('DoctorListSearchItemComponent', () => {
  let component: DoctorListSearchItemComponent;
  let fixture: ComponentFixture<DoctorListSearchItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DoctorListSearchItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorListSearchItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
