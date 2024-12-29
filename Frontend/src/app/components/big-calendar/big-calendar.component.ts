import { CommonModule } from '@angular/common';
import { Component, Input, Output, output } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Course } from '../../Domain/course';
import { CourseAssignementComponentService } from '../../Services/CourseAssignement';
import { CourseService } from '../../Services/courses.service';
import { CourseAssigment } from '../../Domain/courseAssignement';

@Component({
  selector: 'app-big-calendar',
  imports: [CommonModule],
  templateUrl: './big-calendar.component.html',
  styleUrl: './big-calendar.component.css',
})
export class BigCalendarComponent {
  @Input() startMonth: number = 0;
  @Input() startYear: number = 0;
  @Input() selectedStartDay: number = 0;
  @Output() selectedDate: EventEmitter<any> = new EventEmitter<any>();
  listCourseAssignment: CourseAssigment[] = [];
  listCourses: Course[] = [];
  onDateSelected(day: number, id: number) {
    var courseAssigment = new CourseAssigment();
    this.listCourseAssignment.forEach((x) => {
      if (id == x.id) courseAssigment = x;
    });
    var date = new Date(this.year, this.month, day);
    this.selectedDate.emit({ date, courseAssigment });
  }
  currentDate: Date = new Date();
  year: number = this.currentDate.getFullYear();
  month: number = this.currentDate.getMonth();

  weekDays: string[] = ['Nd', 'Pn', 'Wt', 'Sr', 'Cz', 'Pt', 'So'];
  monthNames: string[] = [
    'Styczeń',
    'Luty',
    'Marzec',
    'Kwiecień',
    'Maj',
    'Czerwiec',
    'Lipiec',
    'Sierpień',
    'Wrzesień',
    'Październik',
    'Listopad',
    'Grudzień',
  ];
  days: Array<{
    date: number;
    dayOfWeek: number;
    isCurrentMonth: boolean;
    courseNameToDisplay: string;
    courseAssigmentId: number;
  }> = [];

  constructor(
    private courseAssigmentService: CourseAssignementComponentService,
    private courseService: CourseService
  ) {}
  ngOnInit(): void {
    this.month = this.startMonth;
    this.year = this.startYear;
    this.courseService.getAllCourses().subscribe({
      next: (data) => {
        this.listCourses = data;
        this.generateCalendar();
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });
    this.courseAssigmentService.getAllCourseAssignments().subscribe({
      next: (data) => {
        this.listCourseAssignment = data;
        this.generateCalendar();
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });
  }

  generateCalendar() {
    this.days = [];
    let firstDay = new Date(this.year, this.month, 1);
    let lastDay = new Date(this.year, this.month + 1, 0);
    let startDay = firstDay.getDay();
    let prevMonthLastDate = new Date(this.year, this.month, 0).getDate();
    for (let i = startDay - 1; i >= 0; i--) {
      var displayCourse = '';
      var keepcourseId = 0;
      this.listCourseAssignment.forEach((x) => {
        let checkDate = new Date(
          this.year,
          this.month - 1,
          prevMonthLastDate - i
        );
        checkDate.setHours(0, 0, 0, 0);

        let startDate = new Date(x.startDate);
        let endDate = new Date(x.endDate);
        startDate.setHours(0, 0, 0, 0);
        endDate.setHours(0, 0, 0, 0);
        if (checkDate >= startDate && checkDate < endDate) {
          this.listCourses.forEach((y) => {
            if (x.courseId == y.id) {
              displayCourse = y.name;
              keepcourseId = x.id;
            }
          });
        }
      });

      this.days.push({
        date: prevMonthLastDate - i,
        dayOfWeek: i,
        isCurrentMonth: false,
        courseNameToDisplay: displayCourse,
        courseAssigmentId: keepcourseId,
      });
    }
    for (let date = 1; date <= lastDay.getDate(); date++) {
      let dayOfWeek = new Date(this.year, this.month, date).getDay();
      var displayCourse = '';
      var keepcourseId = 0;
      this.listCourseAssignment.forEach((x) => {
        let checkDate = new Date(this.year, this.month, date);
        checkDate.setHours(0, 0, 0, 0);

        let startDate = new Date(x.startDate);
        let endDate = new Date(x.endDate);
        startDate.setHours(0, 0, 0, 0);
        endDate.setHours(0, 0, 0, 0);

        if (checkDate >= startDate && checkDate < endDate) {
          this.listCourses.forEach((y) => {
            if (x.courseId == y.id) {
              displayCourse = y.name;
              keepcourseId = x.id;
            }
          });
        }
      });
      this.days.push({
        date,
        dayOfWeek,
        isCurrentMonth: true,
        courseNameToDisplay: displayCourse,
        courseAssigmentId: keepcourseId,
      });
    }

    let totalDays = this.days.length;
    let requiredRows = Math.ceil(totalDays / 7);

    for (let i = totalDays; i < requiredRows * 7; i++) {
      var displayCourse = '';
      var keepcourseId = 0;
      this.listCourseAssignment.forEach((x) => {
        let checkDate = new Date(this.year, this.month, i);
        checkDate.setHours(0, 0, 0, 0);

        let startDate = new Date(x.startDate);
        let endDate = new Date(x.endDate);
        startDate.setHours(0, 0, 0, 0);
        endDate.setHours(0, 0, 0, 0);
        if (checkDate >= startDate && checkDate < endDate) {
          this.listCourses.forEach((y) => {
            if (x.courseId == y.id) {
              displayCourse = y.name;
              keepcourseId = x.id;
            }
          });
        }
      });
      this.days.push({
        date: i - totalDays + 1,
        dayOfWeek: i % 7,
        isCurrentMonth: false,
        courseNameToDisplay: displayCourse,
        courseAssigmentId: keepcourseId,
      });
    }
  }

  previousMonth() {
    if (this.month === 0) {
      this.month = 11;
      this.year--;
    } else {
      this.month--;
    }
    this.generateCalendar();
  }

  nextMonth() {
    if (this.month === 11) {
      this.month = 0;
      this.year++;
    } else {
      this.month++;
    }
    this.generateCalendar();
  }

  isSelectedDay(day: { date: number; isCurrentMonth: boolean }): boolean {
    return (
      this.selectedStartDay === day.date &&
      this.startMonth === this.month &&
      this.startYear === this.year &&
      day.isCurrentMonth
    );
  }
  isToday(day: { date: number; isCurrentMonth: boolean }) {
    let today = new Date();
    return (
      today.getDate() === day.date &&
      today.getMonth() === this.month &&
      today.getFullYear() === this.year &&
      day.isCurrentMonth
    );
  }
}
