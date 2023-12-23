import { TestBed } from '@angular/core/testing';

import { LearningservicesService } from './learningservices.service';

describe('LearningservicesService', () => {
  let service: LearningservicesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LearningservicesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
