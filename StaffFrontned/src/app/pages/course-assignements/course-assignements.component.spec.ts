import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseAssignementsComponent } from './course-assignements.component';

describe('CourseAssignementsComponent', () => {
  let component: CourseAssignementsComponent;
  let fixture: ComponentFixture<CourseAssignementsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CourseAssignementsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CourseAssignementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
