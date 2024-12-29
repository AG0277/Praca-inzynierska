import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Instructor } from '../../Domain/instructor';
import { InstructorService } from '../../services/instructor.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-instructor',
  imports: [FormsModule],
  templateUrl: './instructor.component.html',
  styleUrl: './instructor.component.css',
})
export class InstructorComponent {
  @Input() instructor: Instructor = new Instructor(0, '', '', '');
  @Output() delete: EventEmitter<Instructor> = new EventEmitter<Instructor>();
  updateInstructor() {
    if (this.instructor.id == 0) {
      this.instructorService.createInstructor(this.instructor).subscribe({
        next: (data) => {},
        error: (err) => {
          console.error('Error fetching rooms:', err);
        },
      });
    } else {
      this.instructorService.updateInstructor(this.instructor).subscribe({
        next: (data) => {},
        error: (err) => {
          console.error('Error fetching rooms:', err);
        },
      });
    }
  }

  constructor(private instructorService: InstructorService) {}
  deleteInstructor() {
    this.instructorService.deleteInstructor(this.instructor.id).subscribe({
      next: (data) => {
        this.delete.emit(this.instructor);
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });
  }
}
