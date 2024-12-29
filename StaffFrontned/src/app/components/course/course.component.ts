import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Course } from '../../Domain/course';
import { FormsModule } from '@angular/forms';
import { CourseService } from '../../services/course.service';

@Component({
  selector: 'app-course',
  imports: [FormsModule],
  templateUrl: './course.component.html',
  styleUrl: './course.component.css',
})
export class CourseComponent {
  @Input() course: Course = new Course(0, '', '', 0, '', 0);
  @Output() delete: EventEmitter<Course> = new EventEmitter();
  updateCourse() {
    if (this.course.id == 0) {
      this.courseService.createCourse(this.course).subscribe({
        next: (data) => {},
        error: (err) => {
          console.error('Error fetching rooms:', err);
        },
      });
    } else {
      this.courseService.updateCourse(this.course).subscribe({
        next: (data) => {},
        error: (err) => {
          console.error('Error fetching rooms:', err);
        },
      });
    }
  }

  constructor(private courseService: CourseService) {}
  deleteCourse() {
    this.courseService.deleteCourse(this.course.id).subscribe({
      next: (data) => {
        this.delete.emit(this.course);
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });
  }
}
