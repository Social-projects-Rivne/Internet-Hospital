import { TestBed } from '@angular/core/testing';

import { ImageValidationService } from './image-validation.service';

describe('ImageValidationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ImageValidationService = TestBed.get(ImageValidationService);
    expect(service).toBeTruthy();
  });
});
