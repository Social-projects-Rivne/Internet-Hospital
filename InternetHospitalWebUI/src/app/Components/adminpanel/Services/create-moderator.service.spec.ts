import { TestBed } from '@angular/core/testing';

import { CreateModeratorService } from './create-moderator.service';

describe('CreateModeratorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CreateModeratorService = TestBed.get(CreateModeratorService);
    expect(service).toBeTruthy();
  });
});
