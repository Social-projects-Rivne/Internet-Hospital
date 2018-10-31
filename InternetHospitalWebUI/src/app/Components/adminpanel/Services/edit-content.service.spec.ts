import { TestBed } from '@angular/core/testing';

import { EditContentService } from './edit-content.service';

describe('EditContentService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EditContentService = TestBed.get(EditContentService);
    expect(service).toBeTruthy();
  });
});
