import { TestBed } from '@angular/core/testing';

import { ImageHandlingService } from './image-handling.service';

describe('ImageHandlingService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ImageHandlingService = TestBed.get(ImageHandlingService);
    expect(service).toBeTruthy();
  });
});
