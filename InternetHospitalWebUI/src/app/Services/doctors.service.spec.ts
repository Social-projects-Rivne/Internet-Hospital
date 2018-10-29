import { TestBed } from '@angular/core/testing';

import { DoctorsService } from './doctors.service';

describe('DoctorsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DoctorsService = TestBed.get(DoctorsService);
    expect(service).toBeTruthy();
  });
});
