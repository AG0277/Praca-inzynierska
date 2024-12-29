import { Component } from '@angular/core';
import { UserReservationService } from '../../services/userReservation.service';
import {
  mapToRoomAndRoomReservation,
  RoomAndRoomReservation,
  RoomReservation,
} from '../../Domain/reservation';
import { RoomsService } from '../../services/rooms.service';
import { Room } from '../../Domain/room';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-reservations',
  imports: [CommonModule, FormsModule],
  templateUrl: './user-reservations.component.html',
  styleUrl: './user-reservations.component.css',
})
export class UserReservationsComponent {
  constructor(
    private userReservationService: UserReservationService,
    private roomsService: RoomsService
  ) {}
  numerTelefonu: string = '';

  roomReservation: RoomReservation[] = [];
  //rooms: Room[] = [];
  roomAndRoomReservations: RoomAndRoomReservation[] = [];
  ngOnInit() {}

  SearchPhoneNumber() {
    console.log('Wchodze');
    console.log(this.numerTelefonu);
    this.userReservationService
      .getRoomReservation(this.numerTelefonu)
      .subscribe({
        next: (data) => {
          this.roomReservation = data;
          this.roomReservation.forEach((x) => {
            this.roomsService.getRoomById(x.roomId).subscribe({
              next: (data) => {
                this.roomAndRoomReservations.push(
                  mapToRoomAndRoomReservation(x, data)
                );
                console.log(this.roomAndRoomReservations);
              },
              error: (err) => {
                console.error('Error fetching rooms:', err);
              },
            });
          });
        },
        error: (err) => {
          console.error('Error fetching rooms:', err);
        },
      });
  }
}
