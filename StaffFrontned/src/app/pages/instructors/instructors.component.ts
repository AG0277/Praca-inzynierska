import { Component } from '@angular/core';
import { Instructor } from '../../Domain/instructor';
import { InstructorService } from '../../services/instructor.service';
import { InstructorComponent } from '../../components/instructor/instructor.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-instructors',
  imports: [InstructorComponent, CommonModule],
  templateUrl: './instructors.component.html',
  styleUrl: './instructors.component.css',
})
export class InstructorsComponent {
  instructorDic: { [key: string]: Instructor } = {};
  constructor(private instructorService: InstructorService) {}
  ngOnInit() {
    this.instructorService.getAllInstructors().subscribe({
      next: (data) => {
        this.instructorDic = this.toDic(data);
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });
  }

  deleteInstructor(instructor: Instructor) {
    delete this.instructorDic[instructor.id];
  }
  createInstructor() {
    if (this.instructorDic[0]) return;
    var instructor = new Instructor(0, '', '', '');
    this.instructorDic[instructor.id] = instructor;
  }

  toDic(courses: Instructor[]): { [key: string]: Instructor } {
    const dictionary: { [key: string]: Instructor } = {};

    for (const course of courses) {
      dictionary[course.id] = course;
    }

    return dictionary;
  }
}
