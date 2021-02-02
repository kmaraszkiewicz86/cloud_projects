import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DynamoDbDetailsComponent } from './dynamo-db-details.component';

describe('DynamoDbDetailsComponent', () => {
  let component: DynamoDbDetailsComponent;
  let fixture: ComponentFixture<DynamoDbDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DynamoDbDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DynamoDbDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
