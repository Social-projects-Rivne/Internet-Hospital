import { TestBed } from '@angular/core/testing';

import { FeedBackService } from './feed-back.service';

describe('FeedBackService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FeedBackService = TestBed.get(FeedBackService);
    expect(service).toBeTruthy();
  });
});
