import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DynamoDbListComponent } from './dynamo-db-list.component';

describe('DynamoDbListComponent', () => {
  let component: DynamoDbListComponent;
  let fixture: ComponentFixture<DynamoDbListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DynamoDbListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DynamoDbListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
