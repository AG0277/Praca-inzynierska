import { Component } from '@angular/core';
import { RoomsService } from '../../services/rooms.service';
import { HttpClient } from '@angular/common/http';
import { CreateRoomDto, Room, UpdateRoomDto } from '../../Domain/room';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { compress, decompress } from 'lz-string';

@Component({
  selector: 'app-rooms',
  imports: [CommonModule, FormsModule],
  providers: [HttpClient, RoomsService],
  templateUrl: './rooms.component.html',
  styleUrl: './rooms.component.css',
})
export class RoomsComponent {
  rooms: { [key: number]: Room } = {};
  selectedFile: File | null = null;

  constructor(private RoomsServices: RoomsService) {}
  ngOnInit() {
    this.RoomsServices.getRooms().subscribe({
      next: (data) => {
        this.rooms = this.arrayToDictionary(data);
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });
  }

  addOrUpdateRoom(room: Room): void {
    if (room.id == 0) {
      var dto = new CreateRoomDto(
        room.numberOfBeds,
        room.roomNumber,
        room.pricePerBed,
        room.imageBase64
      );
      this.RoomsServices.createRoom(dto).subscribe({
        next: (data) => {
          console.log(data);
          this.rooms[data.id] = data;
          delete this.rooms[0];
        },
        error: (err) => {
          console.error('Error fetching rooms:', err);
        },
      });
    } else {
      var updateDto = new UpdateRoomDto(
        room.id,
        room.numberOfBeds,
        room.roomNumber,
        room.pricePerBed,
        room.imageBase64
      );
      this.RoomsServices.updateRoom(updateDto).subscribe({
        next: (data) => {},
        error: (err) => {
          console.error('Error fetching rooms:', err);
        },
      });
    }
  }
  deleteRoom(id: number): void {
    this.RoomsServices.deleteRoom(id).subscribe({
      next: (data) => {
        delete this.rooms[id];
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });
  }
  createRoom(): void {
    var room = new Room(0, 0, 0, 0, '');
    if (!(0 in this.rooms)) {
      this.rooms[0] = room;
    }
  }

  private arrayToDictionary(rooms: Room[]): { [key: number]: Room } {
    return rooms.reduce((acc, room) => {
      acc[room.id] = room;
      return acc;
    }, {} as { [key: number]: Room });
  }

  onFileSelected(event: Event, room: Room): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        room.imageBase64 = reader.result as string;
      };
      reader.readAsDataURL(this.selectedFile);
    }
  }
}
