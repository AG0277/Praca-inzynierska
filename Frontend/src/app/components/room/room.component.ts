import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Room } from '../../Domain/room';
import { RoomReservationDto } from '../../Domain/ReservationDto';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-room',
  imports: [CommonModule],
  templateUrl: './room.component.html',
  styleUrl: './room.component.css',
})
export class RoomComponent {
  @Input() room: Room = new Room(0, 0, 0, 0, '', 0);
  @Output() roomReservation: EventEmitter<RoomReservationDto> =
    new EventEmitter<RoomReservationDto>();
  onReservation(noOfPeople: number, roomId: number) {
    var room = new RoomReservationDto();
    room.noOfPeople = noOfPeople;
    room.roomId = roomId;
    room.roomNumber = this.room.roomNumber;
    room.totalPrice = this.room.pricePerBed;
    room.availableSpace =
      this.room.numberOfBeds - this.room.numberOfReservedBeds;
    this.roomReservation.emit(room);
  }
  howManyRooms: number = 0;
  incrementHowManyRooms() {
    if (
      this.howManyRooms <
      this.room.numberOfBeds - this.room.numberOfReservedBeds
    )
      this.howManyRooms++;
  }
  decrementHowManyRooms() {
    if (this.howManyRooms > 0) this.howManyRooms--;
  }
}
