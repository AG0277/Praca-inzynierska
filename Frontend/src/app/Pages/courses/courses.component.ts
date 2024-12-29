import { Component } from '@angular/core';
import { Course } from '../../Domain/course';
import { CourseService } from '../../Services/courses.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-courses',
  imports: [CommonModule, FormsModule],
  templateUrl: './courses.component.html',
  styleUrl: './courses.component.css',
})
export class CoursesComponent {
  courseList: Course[] = [];
  course: Course = new Course(0, '', '', 0, '', 0);
  selectedCourseId: number | null = null;
  constructor(private courseService: CourseService) {}
  courseSeasonType = 'Undefined';
  ngOnInit() {
    this.courseService.getAllCourses().subscribe({
      next: (data) => {
        if (data.length > 0) {
          this.course = data[0];
          this.courseSeasonType = this.getCourseTranslated(
            this.course.seasonType
          );
        }
        this.courseList = data;
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });
  }

  getCourseTranslated(season: string): string {
    if (season == 'BOTH') return 'Całoroczny';
    else if (season == 'WINTER') return 'Zimowy';
    else if (season == 'SUMMER') return 'Letni';
    return 'undefined';
  }

  onCourseChange() {
    if (this.selectedCourseId) {
      this.courseList.forEach((x) => {
        if (x.id == this.selectedCourseId) {
          this.course = x;
        }
      });
    }
  }
}
