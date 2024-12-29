import { Component } from '@angular/core';
import { CourseComponent } from '../../components/course/course.component';
import { Course } from '../../Domain/course';
import { CommonModule } from '@angular/common';
import { CourseService } from '../../services/course.service';

@Component({
  selector: 'app-courses',
  imports: [CourseComponent, CommonModule],
  templateUrl: './courses.component.html',
  styleUrl: './courses.component.css',
})
export class CoursesComponent {
  courseDic: { [key: string]: Course } = {};
  constructor(private courseService: CourseService) {}
  ngOnInit() {
    this.courseService.getAllCourses().subscribe({
      next: (data) => {
        this.courseDic = this.toDic(data);
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });
  }

  deleteCourse(course: Course) {
    delete this.courseDic[course.id];
  }
  createCourse() {
    if (this.courseDic[0]) return;
    var course = new Course(0, '', '', 0, '', 0);
    this.courseDic[course.id] = course;
  }

  toDic(courses: Course[]): { [key: string]: Course } {
    const dictionary: { [key: string]: Course } = {};

    for (const course of courses) {
      dictionary[course.id] = course;
    }

    return dictionary;
  }
}
