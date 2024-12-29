import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { RoomReservationDto } from '../../Domain/ReservationDto';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
  providedIn: 'root',
})
export class RoomReservationSessionDataService {
  private storageKey = 'roomReservations';
  constructor(@Inject(PLATFORM_ID) private platformId: Object) {}
  saveReservations(reservations: RoomReservationDto[]): void {
    if (isPlatformBrowser(this.platformId)) {
      const data = JSON.stringify(reservations);
      sessionStorage.setItem(this.storageKey, data);
    }
  }
  addReservation(reservation: RoomReservationDto): void {
    if (isPlatformBrowser(this.platformId)) {
      const reservations = this.getReservations();
      reservations.push(reservation);
      this.saveReservations(reservations);
    }
  }

  getReservations(): RoomReservationDto[] {
    if (isPlatformBrowser(this.platformId)) {
      const data = sessionStorage.getItem(this.storageKey);
      if (data) {
        return JSON.parse(data) as RoomReservationDto[];
      }
      return [];
    }
    return [];
  }

  removeReservation(index: number): void {
    if (isPlatformBrowser(this.platformId)) {
      const reservations = this.getReservations();
      reservations.splice(index, 1);
      this.saveReservations(reservations);
    }
  }
  clearReservations(): void {
    if (isPlatformBrowser(this.platformId)) {
      sessionStorage.removeItem(this.storageKey);
    }
  }
}
