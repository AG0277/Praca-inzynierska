import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Course } from '../../Domain/course';
import { Instructor } from '../../Domain/instructor';
import { FormsModule } from '@angular/forms';
import { CourseAssignementComponentService } from '../../services/courseAssignment.service';
import {
  CourseAssigment,
  CreateCourseAssigment,
} from '../../Domain/courseAssignement';
import { CourseService } from '../../services/course.service';
import { InstructorService } from '../../services/instructor.service';

@Component({
  selector: 'app-course-assignement',
  imports: [CommonModule, FormsModule],
  templateUrl: './course-assignement.component.html',
  styleUrl: './course-assignement.component.css',
})
export class CourseAssignementComponent {
  @Input() isPopupVisible = true;
  @Input() selectedDate: Date = new Date();
  @Input() startDate: Date = new Date();
  @Input() endDate: Date = new Date();
  @Input() courseId: number = 0;
  @Input() instructorId: number = 0;
  @Input() coursAssigmentId: number = 0;
  @Output() notifyToTurnOff = new EventEmitter<void>();

  constructor(
    private courseAssignement: CourseAssignementComponentService,
    private courseservice: CourseService,
    private instructorService: InstructorService
  ) {}
  instructorList: Instructor[] = [];
  courseList: Course[] = [];

  ngOnInit(): void {
    this.courseservice.getAllCourses().subscribe({
      next: (data) => {
        this.courseList = data;
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });

    this.instructorService.getAllInstructors().subscribe({
      next: (data) => {
        this.instructorList = data;
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });
  }
  SaveOrUpdate() {
    var dto = new CreateCourseAssigment();
    dto.initialize(
      this.courseId,
      this.instructorId,
      this.startDate,
      this.endDate
    );
    if (this.coursAssigmentId == 0 || this.coursAssigmentId == undefined) {
      this.courseAssignement.createCourseAssignment(dto).subscribe({
        next: (data) => {
          this.closePopup();
        },
        error: (err) => {
          console.error('Error fetching rooms:', err);
        },
      });
    } else {
      var updateDto = new CourseAssigment();
      updateDto.initialize(
        this.coursAssigmentId,
        this.courseId,
        this.instructorId,
        this.startDate,
        this.endDate
      );
      this.courseAssignement.updateCourseAssigment(updateDto).subscribe({
        next: (data) => {
          this.closePopup();
        },
        error: (err) => {
          console.error('Error fetching rooms:', err);
        },
      });
    }
  }

  delete() {
    this.courseAssignement
      .deleteCourseAssigment(this.coursAssigmentId)
      .subscribe({
        next: (data) => {
          this.closePopup();
        },
        error: (err) => {
          console.error('Error fetching rooms:', err);
        },
      });
  }
  closePopup() {
    this.notifyToTurnOff.emit();
  }
  onInstructorChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.instructorId = Number(selectElement.value);
  }

  onCourseChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.courseId = Number(selectElement.value);
    this.courseList.forEach((x) => {
      if (x.id == this.courseId) {
        this.startDate = this.selectedDate;
        this.endDate = new Date(this.selectedDate);
        this.endDate.setDate(this.endDate.getDate() + x.numberOfDays);
      }
    });
  }
}
