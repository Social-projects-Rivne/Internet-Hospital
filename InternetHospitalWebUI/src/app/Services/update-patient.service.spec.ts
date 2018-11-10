import { TestBed } from '@angular/core/testing';

import { UpdatePatientService } from './update-patient.service';

describe('UpdatePatientService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UpdatePatientService = TestBed.get(UpdatePatientService);
    expect(service).toBeTruthy();
  });
});
