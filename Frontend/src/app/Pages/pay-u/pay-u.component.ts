import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { RoomReservationSessionDataService } from '../../Services/roomReservation/room-reservation-session-data-service';
import {
  CreateRoomReservationDto,
  mapToCreateRoomReservationDto,
  RoomReservationDto,
} from '../../Domain/ReservationDto';
import { CommonModule } from '@angular/common';
import { AppUserService } from '../../Services/appUser/app-user-service';
import { AppUser, CreateAppUserDto } from '../../Domain/appUser';
import { RoomsReservationService } from '../../Services/roomReservation/room-reservation-service';
import { error } from 'console';

@Component({
  selector: 'app-pay-u',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './pay-u.component.html',
  styleUrl: './pay-u.component.css',
})
export class PayUComponent {
  contactForm!: FormGroup;
  reservationList: RoomReservationDto[] = [];

  constructor(
    private roomReservationSessionDataServiceService: RoomReservationSessionDataService,
    private appUserService: AppUserService,
    private roomReservationService: RoomsReservationService
  ) {
    this.reservationList =
      roomReservationSessionDataServiceService.getReservations();
  }
  ngOnInit() {
    this.contactForm = new FormGroup({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      phone: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required]),
    });
  }
  onSubmit() {
    if (this.contactForm.valid) {
      var createUserDto = new CreateAppUserDto();
      createUserDto.firstName = this.contactForm.value.firstName;
      createUserDto.lastName = this.contactForm.value.lastName;
      createUserDto.phoneNumber = this.contactForm.value.phone;
      createUserDto.email = this.contactForm.value.email;

      this.appUserService
        .getAppUserByPhone(createUserDto.phoneNumber)
        .subscribe({
          next: (data) => {
            this.CreateUser(data, createUserDto);
          },
        });
    }
  }

  CreateUser(user: AppUser, createUserDto: CreateAppUserDto) {
    if (user.id == 0) {
      this.appUserService.createAppUser(createUserDto).subscribe({
        next: (data) => {
          this.CreateRoom(data.id);
        },
        error: (err) => {
          console.error('Error creating user:', err);
        },
      });
    } else {
      this.CreateRoom(user.id);
    }
  }
  CreateRoom(appUserId: number) {
    this.reservationList.forEach((room) => {
      const roomDto = mapToCreateRoomReservationDto(room, appUserId);
      console.log(roomDto);
      this.roomReservationService.createRoomReservation(roomDto).subscribe({
        next: () => {
          console.log(`Room reservation created for roomId: ${roomDto.roomId}`);
          this.reservationList = [];
        },
        error: (reservationErr) => {
          console.error('Error creating room reservation:', reservationErr);
        },
      });
    });
  }
  removeFromCart(i: number) {
    this.roomReservationSessionDataServiceService.removeReservation(i);
    this.reservationList =
      this.roomReservationSessionDataServiceService.getReservations();
  }
}
