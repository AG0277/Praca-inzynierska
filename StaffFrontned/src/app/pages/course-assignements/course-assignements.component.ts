import { Component } from '@angular/core';
import { SmallCalendarComponent } from '../../components/small-calendar/small-calendar.component';
import { CourseAssignementComponent } from '../../components/course-assignement/course-assignement.component';
import { CourseAssignementComponentService } from '../../services/courseAssignment.service';
import { CourseAssigment } from '../../Domain/courseAssignement';

@Component({
  selector: 'app-course-assignements',
  imports: [SmallCalendarComponent, CourseAssignementComponent],
  templateUrl: './course-assignements.component.html',
  styleUrl: './course-assignements.component.css',
})
export class CourseAssignementsComponent {
  startDate: Date = new Date();
  fromYear: number = 0;
  fromNumberMonth: number = 0;
  fromDay: number = 0;

  modalStartDate: Date = new Date();
  modalEndDate: Date = new Date();
  courseId: number = 0;
  instructorId: number = 0;
  isPopupVisible: boolean = false;
  selectedDate: Date = new Date();
  coursAssigmentId: number = 0;

  listCourseAssignment: CourseAssigment[] = [];

  constructor(
    private courseAssigmentService: CourseAssignementComponentService
  ) {}
  ngOnInit(): void {
    this.fromDay = this.startDate.getDate();
    this.fromYear = this.startDate.getFullYear();
    this.fromNumberMonth = this.startDate.getMonth();
    this.courseAssigmentService.getAllCourseAssignments().subscribe({
      next: (data) => {
        this.listCourseAssignment = data;
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });
  }

  onFromDateSelected(event: {
    date: Date;
    courseAssigment: CourseAssigment;
  }): void {
    this.selectedDate = event.date;
    this.isPopupVisible = true;
    this.courseId = event.courseAssigment.courseId;
    this.instructorId = event.courseAssigment.instructorId;
    this.modalStartDate = event.courseAssigment.startDate;
    this.modalEndDate = event.courseAssigment.endDate;
    this.coursAssigmentId = event.courseAssigment.id;
  }

  closePopUp() {
    this.isPopupVisible = false;
    this.coursAssigmentId = 0;
    this.courseAssigmentService.getAllCourseAssignments().subscribe({
      next: (data) => {
        this.listCourseAssignment = data;
        window.location.reload();
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });
  }
}
