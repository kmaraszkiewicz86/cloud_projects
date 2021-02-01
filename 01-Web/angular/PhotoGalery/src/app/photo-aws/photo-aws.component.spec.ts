import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhotoAwsComponent } from './photo-aws.component';

describe('PhotoAwsComponent', () => {
  let component: PhotoAwsComponent;
  let fixture: ComponentFixture<PhotoAwsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhotoAwsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PhotoAwsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
