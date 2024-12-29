import {
  HttpClient,
  HttpErrorResponse,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of, throwError } from 'rxjs';
import { environment } from '../../environment';
import { Instructor } from '../Domain/instructor';

@Injectable({
  providedIn: 'root',
})
export class InstructorService {
  private apiUrl = environment.apiUrl + 'Instructor/';
  constructor(private client: HttpClient) {}
  getAllInstructors(): Observable<Instructor[]> {
    return this.client
      .get<Instructor[]>(this.apiUrl + 'GetAllInstructors', {
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
          if (error.status === 404) return of([]);
          console.error('Error in HTTP request:', error);
          return throwError(
            () => new Error('Something went wrong. Please try again later.')
          );
        })
      );
  }

  getInstructorById(id: number): Observable<Instructor> {
    return this.client
      .get<Instructor[]>(this.apiUrl + `GetInstructorById/${id}`, {
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
          if (error.status === 404) return of(new Instructor(0, '', '', ''));
          console.error('Error in HTTP request:', error);
          return throwError(
            () => new Error('Something went wrong. Please try again later.')
          );
        })
      );
  }
}
