import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayUComponent } from './pay-u.component';

describe('PayUComponent', () => {
  let component: PayUComponent;
  let fixture: ComponentFixture<PayUComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PayUComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PayUComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
