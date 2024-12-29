import {
  HttpClient,
  HttpErrorResponse,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, throwError } from 'rxjs';
import { environment } from '../../../environment';
import { Room } from '../../Domain/room';
import { CreateRoomReservationDto } from '../../Domain/ReservationDto';

@Injectable({
  providedIn: 'root',
})
export class RoomsReservationService {
  private apiUrl = environment.apiUrl + 'RoomReservation/';
  constructor(private client: HttpClient) {}
  createRoomReservation(roomDto: CreateRoomReservationDto): Observable<Room[]> {
    return this.client
      .post<any>(this.apiUrl + 'CreateRoomReservation', roomDto, {
        observe: 'response',
      })
      .pipe(
        map((response: HttpResponse<any>) => {
          if (response.status === 200) {
            return response.body;
          } else {
            throw new Error(`Unexpected response status: ${response.status}`);
          }
        }),
        catchError((error: HttpErrorResponse) => {
          console.error('Error in HTTP request:', error);
          return throwError(
            () => new Error('Something went wrong. Please try again later.')
          );
        })
      );
  }
}
