import { CommonModule } from '@angular/common';
import { Component, Input, Output, output } from '@angular/core';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-small-calendar',
  imports: [CommonModule],
  templateUrl: './small-calendar.component.html',
  styleUrl: './small-calendar.component.css',
})
export class SmallCalendarComponent {
  @Input() startMonth: number = 0;
  @Input() startYear: number = 0;
  @Input() selectedStartDay: number = 0;
  @Output() selectedDate: EventEmitter<any> = new EventEmitter<any>();
  onDateSelected(day: number) {
    var date = new Date(this.year, this.month, day);
    this.selectedDate.emit(date);
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
  }> = [];

  ngOnInit(): void {
    this.month = this.startMonth;
    this.year = this.startYear;
    this.generateCalendar();
  }

  generateCalendar() {
    this.days = [];
    let firstDay = new Date(this.year, this.month, 1);
    let lastDay = new Date(this.year, this.month + 1, 0);

    let startDay = firstDay.getDay();
    let prevMonthLastDate = new Date(this.year, this.month, 0).getDate();
    for (let i = startDay - 1; i >= 0; i--) {
      this.days.push({
        date: prevMonthLastDate - i,
        dayOfWeek: i,
        isCurrentMonth: false,
      });
    }

    for (let date = 1; date <= lastDay.getDate(); date++) {
      let dayOfWeek = new Date(this.year, this.month, date).getDay();
      this.days.push({
        date,
        dayOfWeek,
        isCurrentMonth: true,
      });
    }

    let totalDays = this.days.length;
    let requiredRows = Math.ceil(totalDays / 7);

    for (let i = totalDays; i < requiredRows * 7; i++) {
      this.days.push({
        date: i - totalDays + 1,
        dayOfWeek: i % 7,
        isCurrentMonth: false,
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
