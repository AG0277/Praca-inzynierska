import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  Output,
  SimpleChanges,
} from '@angular/core';
import { Course } from '../../Domain/course';
import { FormsModule } from '@angular/forms';

import { CourseAssignementComponentService } from '../../Services/CourseAssignement';
import { CourseService } from '../../Services/courses.service';
import { InstructorService } from '../../Services/instructorService';
import { Instructor } from '../../Domain/instructor';

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
  course: Course = new Course(0, '', '', 0, '', 0);
  instructor: Instructor = new Instructor(0, '', '', '');

  constructor(
    private courseAssignement: CourseAssignementComponentService,
    private courseservice: CourseService,
    private instructorService: InstructorService
  ) {}
  instructorList: Instructor[] = [];
  courseList: Course[] = [];

  ngOnInit(): void {
    this.courseservice.getCourseById(this.instructorId).subscribe({
      next: (data) => {
        this.course = data;
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });

    this.instructorService.getInstructorById(this.instructorId).subscribe({
      next: (data) => {
        this.instructor = data;
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });
  }
  closePopup() {
    this.notifyToTurnOff.emit();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['instructorId']) {
      this.handleInstructorChange(changes['instructorId'].currentValue);
    }

    if (changes['coursAssigmentId']) {
      this.handleCourseAssignmentChange(
        changes['coursAssigmentId'].currentValue
      );
    }
  }

  handleInstructorChange(newId: number): void {
    this.instructorService.getInstructorById(newId).subscribe({
      next: (data) => {
        this.instructor = data;
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });
  }

  handleCourseAssignmentChange(newId: number): void {
    this.courseservice.getCourseById(newId).subscribe({
      next: (data) => {
        this.course = data;
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });
  }
}
