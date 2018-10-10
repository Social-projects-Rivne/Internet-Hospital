import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeNewsItemComponent } from './home-news-item.component';

describe('HomeNewsItemComponent', () => {
  let component: HomeNewsItemComponent;
  let fixture: ComponentFixture<HomeNewsItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HomeNewsItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeNewsItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
