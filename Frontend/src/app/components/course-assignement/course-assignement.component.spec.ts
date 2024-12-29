import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseAssignementComponent } from './course-assignement.component';

describe('CourseAssignementComponent', () => {
  let component: CourseAssignementComponent;
  let fixture: ComponentFixture<CourseAssignementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CourseAssignementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CourseAssignementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
