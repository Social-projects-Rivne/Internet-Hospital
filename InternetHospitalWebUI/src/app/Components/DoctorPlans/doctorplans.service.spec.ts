import { TestBed } from '@angular/core/testing';

import { DoctorplansService } from './doctorplans.service';

describe('DoctorplansService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DoctorplansService = TestBed.get(DoctorplansService);
    expect(service).toBeTruthy();
  });
});
