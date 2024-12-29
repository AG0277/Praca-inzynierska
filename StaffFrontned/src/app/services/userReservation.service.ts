import {
  HttpClient,
  HttpErrorResponse,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of, throwError } from 'rxjs';
import { environment } from '../../environment';
import { RoomReservation } from '../Domain/reservation';

@Injectable({
  providedIn: 'root',
})
export class UserReservationService {
  private apiUrl = environment.apiUrl + 'RoomReservation/';
  constructor(private client: HttpClient) {}
  getRoomReservation(phoneNumber: string): Observable<RoomReservation[]> {
    return this.client
      .get<RoomReservation[]>(
        this.apiUrl + `GetRoomReservationByUserPhoneNumber/${phoneNumber}`,
        {
          observe: 'response',
        }
      )
      .pipe(
        map((response: HttpResponse<any>) => {
          if (response.status === 200) {
            return response.body;
          } else {
            throw new Error(`Unexpected response status: ${response.status}`);
          }
        }),
        catchError((error: HttpErrorResponse) => {
          if (error.status == 404) {
            return of([] as RoomReservation[]);
          }
          console.error('Error in HTTP request:', error);
          return throwError(
            () => new Error('Something went wrong. Please try again later.')
          );
        })
      );
  }
}
