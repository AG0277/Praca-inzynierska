import {
  HttpClient,
  HttpErrorResponse,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, throwError } from 'rxjs';
import { environment } from '../../environment';
import { CreateRoomDto, Room, UpdateRoomDto } from '../Domain/room';
import { handleError } from './common';

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

  getRoomById(id: number): Observable<Room> {
    return this.client
      .get<Room>(this.apiUrl + `GetRoomById/${id}`, { observe: 'response' })
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

  updateRoom(updateRoomDto: UpdateRoomDto): Observable<boolean> {
    return this.client
      .put<boolean>(this.apiUrl + 'UpdateRoom', updateRoomDto, {
        observe: 'response',
      })
      .pipe(
        map((response: HttpResponse<boolean>) => {
          if (response.status === 200) {
            return response.body ?? false;
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

  createRoom(room: CreateRoomDto): Observable<Room> {
    return this.client
      .post<Room>(this.apiUrl + 'CreateRoom', room, {
        observe: 'response',
      })
      .pipe(
        map((response: HttpResponse<Room>) => {
          if (response.status === 200) {
            return response.body ?? new Room(0, 0, 0, 0, '');
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

  deleteRoom(id: number): Observable<boolean> {
    return this.client
      .delete<boolean>(this.apiUrl + `deleteRoom/${id}`, {
        observe: 'response',
      })
      .pipe(
        map((response: HttpResponse<boolean>) => {
          if (response.status === 200) {
            return response.body ?? false;
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
