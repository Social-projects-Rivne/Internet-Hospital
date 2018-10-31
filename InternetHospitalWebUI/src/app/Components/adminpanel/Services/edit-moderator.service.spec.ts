import { TestBed } from '@angular/core/testing';

import { EditModeratorService } from './edit-moderator.service';

describe('EditModeratorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EditModeratorService = TestBed.get(EditModeratorService);
    expect(service).toBeTruthy();
  });
});
