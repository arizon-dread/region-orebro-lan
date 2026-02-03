import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InformationItem } from './information-item';

describe('InformationItem', () => {
  let component: InformationItem;
  let fixture: ComponentFixture<InformationItem>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InformationItem]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InformationItem);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
