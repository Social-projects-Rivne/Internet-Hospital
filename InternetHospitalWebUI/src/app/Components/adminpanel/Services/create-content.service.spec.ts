import { TestBed } from '@angular/core/testing';

import { CreateContentService } from './create-content.service';

describe('CreateContentService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CreateContentService = TestBed.get(CreateContentService);
    expect(service).toBeTruthy();
  });
});
