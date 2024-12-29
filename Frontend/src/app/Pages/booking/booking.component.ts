import { Component } from '@angular/core';
import { SmallCalendarComponent } from '../../components/small-calendar/small-calendar.component';
import { CommonModule } from '@angular/common';
import { RoomComponent } from '../../components/room/room.component';
import { Room } from '../../Domain/room';
import { RoomReservationDto } from '../../Domain/ReservationDto';
import { RoomReservationSessionDataService } from '../../Services/roomReservation/room-reservation-session-data-service';
import { RoomsService } from '../../Services/room/rooms.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-booking',
  imports: [SmallCalendarComponent, CommonModule, RoomComponent, RouterModule],
  templateUrl: './booking.component.html',
  styleUrl: './booking.component.css',
})
export class BookingComponent {
  fromDate: Date = new Date();
  toDate: Date = new Date();
  listRooms: Room[] = [];
  roomReservationDto: RoomReservationDto = new RoomReservationDto();

  onReservation(dto: RoomReservationDto) {
    var count = 0;
    this.roomReservationSessionDataService.getReservations().forEach((x) => {
      if (x.roomId == dto.roomId) {
        count += x.noOfPeople;
      }
    });
    if (dto.availableSpace - count <= 0) return;
    dto.from = this.fromDate;
    dto.to = this.toDate;
    var differenceInTime = this.toDate.getTime() - this.fromDate.getTime();
    var howManyDaysNumber = differenceInTime / (1000 * 60 * 60 * 24);
    dto.totalPrice =
      dto.totalPrice * dto.noOfPeople * Math.round(howManyDaysNumber);
    this.roomReservationSessionDataService.addReservation(dto);
    this.reservationList =
      this.roomReservationSessionDataService.getReservations();
  }
  onFromDateSelected(date: Date): void {
    this.setFromDate(date);
    if (date > this.toDate) {
      this.toDate.setFullYear(
        date.getFullYear(),
        date.getMonth(),
        date.getDate() + 1
      );
      this.setToDate(this.toDate);
    }
    this.showFromCalendar = false;
  }

  onToDateSelected(date: Date): void {
    this.setToDate(date);
    this.showToCalendar = false;
  }
  days = [
    'Niedziela',
    'Poniedzialek',
    'Wtorek',
    'Sroda',
    'Czwartek',
    'Piatek',
    'Sobota',
  ];
  months = [
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

  showFromCalendar: boolean = false;
  showToCalendar: boolean = false;

  fromDay: number = 0;
  fromNumberMonth: number = 0;
  fromMonth: string = '';
  fromYear: number = 0;
  fromWeekDay: string = '';

  toDay: number = 0;
  toNumberMonth: number = 0;
  toMonth: string = '';
  toYear: number = 0;
  toWeekDay: string = '';

  reservationList: RoomReservationDto[] = [];
  constructor(
    private roomReservationSessionDataService: RoomReservationSessionDataService,
    private roomsService: RoomsService
  ) {
    this.setFromDate(this.fromDate);
    this.toDate.setDate(this.fromDate.getDate() + 1);
    this.setToDate(this.toDate);
    this.reservationList = roomReservationSessionDataService.getReservations();
  }
  ngOnInit() {
    this.roomsService
      .getRoomsBetweenDates(this.fromDate, this.toDate)
      .subscribe({
        next: (data) => {
          this.listRooms = data;
        },
        error: (err) => {
          console.error('Error fetching rooms:', err);
        },
      });
  }
  setFromDate(date: Date) {
    this.fromDate.setDate(date.getDate());
    this.fromDay = date.getDate();
    this.fromNumberMonth = date.getMonth();
    this.fromMonth = this.months[date.getMonth()];
    this.fromYear = date.getFullYear();
    this.fromWeekDay = this.days[date.getDay()];
    this.roomsService
      .getRoomsBetweenDates(this.fromDate, this.toDate)
      .subscribe({
        next: (data) => {
          this.listRooms = data;
        },
        error: (err) => {
          console.error('Error fetching rooms:', err);
        },
      });
  }

  setToDate(date: Date) {
    this.toDate.setDate(date.getDate());
    this.toDay = date.getDate();
    this.toNumberMonth = date.getMonth();
    this.toMonth = this.months[date.getMonth()];
    this.toYear = date.getFullYear();
    this.toWeekDay = this.days[date.getDay()];
    this.roomsService
      .getRoomsBetweenDates(this.fromDate, this.toDate)
      .subscribe({
        next: (data) => {
          this.listRooms = data;
        },
        error: (err) => {
          console.error('Error fetching rooms:', err);
        },
      });
  }

  removeFromCart(i: number) {
    this.roomReservationSessionDataService.removeReservation(i);
    this.reservationList =
      this.roomReservationSessionDataService.getReservations();
  }
}
