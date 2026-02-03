import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddInformation } from './add-information';

describe('AddInformation', () => {
  let component: AddInformation;
  let fixture: ComponentFixture<AddInformation>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddInformation]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddInformation);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
