import {
  HttpClient,
  HttpErrorResponse,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of, throwError } from 'rxjs';
import { environment } from '../../../environment';
import { Room } from '../../Domain/room';
import { AppUser, CreateAppUserDto } from '../../Domain/appUser';

@Injectable({
  providedIn: 'root',
})
export class AppUserService {
  private apiUrl = environment.apiUrl + 'AppUser/';
  constructor(private client: HttpClient) {}
  createAppUser(dto: CreateAppUserDto): Observable<AppUser> {
    return this.client
      .post<AppUser>(this.apiUrl + 'CreateUser', dto, { observe: 'response' })
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

  getAppUserByPhone(number: string): Observable<AppUser> {
    return this.client
      .get<AppUser>(this.apiUrl + `GetByPhoneNumber/${number}`, {
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
        catchError((error: any) => {
          console.error('Error in HTTP request:', error);
          if (error.status === 404) return of(new AppUser());
          return throwError(
            () => new Error('Something went wrong. Please try again later.')
          );
        })
      );
  }
}
