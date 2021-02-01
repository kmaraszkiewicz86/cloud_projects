import { TestBed } from '@angular/core/testing';

import { DynamoDbServiceService } from './dynamo-db-service.service';

describe('DynamoDbServiceService', () => {
  let service: DynamoDbServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DynamoDbServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
