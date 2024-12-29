import { Component } from '@angular/core';
import { CourseAssigment } from '../../Domain/courseAssignement';
import { CourseAssignementComponentService } from '../../Services/CourseAssignement';
import { BigCalendarComponent } from '../../components/big-calendar/big-calendar.component';
import { CourseAssignementComponent } from '../../components/course-assignement/course-assignement.component';

@Component({
  selector: 'app-courses-schedule',
  imports: [BigCalendarComponent, CourseAssignementComponent],
  templateUrl: './courses-schedule.component.html',
  styleUrl: './courses-schedule.component.css',
})
export class CoursesScheduleComponent {
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
