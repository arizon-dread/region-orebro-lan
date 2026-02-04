import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientServer } from './client-server';

describe('ClientServer', () => {
  let component: ClientServer;
  let fixture: ComponentFixture<ClientServer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClientServer]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientServer);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
