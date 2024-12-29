import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, throwError } from 'rxjs';
import { environment } from '../../../environment';
import { Room } from '../../Domain/room';

@Injectable({
  providedIn: 'root',
})
export class RoomsService {
  private apiUrl = environment.apiUrl + 'Room/';
  constructor(private client: HttpClient) {}
  getRooms(): Observable<Room[]> {
    return this.client
      .get<Room[]>(this.apiUrl + 'GetAllRooms', { observe: 'response' })
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

  getRoomsBetweenDates(from: Date, to: Date): Observable<Room[]> {
    return this.client
      .get<Room[]>(
        this.apiUrl +
          `GetAvailableRoomsAtDate?from=${encodeURIComponent(
            from.toISOString()
          )}&to=${encodeURIComponent(to.toISOString())}`,
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
          console.error('Error in HTTP request:', error);
          return throwError(
            () => new Error('Something went wrong. Please try again later.')
          );
        })
      );
  }
}
