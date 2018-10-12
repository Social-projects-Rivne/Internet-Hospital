import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeNewsComponent } from './home-news.component';

describe('HomeNewsComponent', () => {
  let component: HomeNewsComponent;
  let fixture: ComponentFixture<HomeNewsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HomeNewsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeNewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
