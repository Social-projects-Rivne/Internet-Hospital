import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModeratorManagementComponent } from './moderator-management.component';

describe('ModeratorManagementComponent', () => {
  let component: ModeratorManagementComponent;
  let fixture: ComponentFixture<ModeratorManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModeratorManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModeratorManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
