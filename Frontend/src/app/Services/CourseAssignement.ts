import {
  HttpClient,
  HttpErrorResponse,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of, throwError } from 'rxjs';
import { environment } from '../../environment';
import {
  CourseAssigment,
  CreateCourseAssigment,
} from '../Domain/courseAssignement';
import { convertToUTCWithoutShift } from './common';

@Injectable({
  providedIn: 'root',
})
export class CourseAssignementComponentService {
  private apiUrl = environment.apiUrl + 'CourseAssigment/';
  constructor(private client: HttpClient) {}

  createCourseAssignment(
    dto: CreateCourseAssigment
  ): Observable<CourseAssigment> {
    dto.endDate = convertToUTCWithoutShift(dto.endDate);
    dto.startDate = convertToUTCWithoutShift(dto.startDate);
    return this.client
      .post<CourseAssigment>(this.apiUrl + 'CreateCourseAssigment', dto, {
        observe: 'response',
      })
      .pipe(
        map((response: HttpResponse<CourseAssigment>) => {
          if (response.status === 200) {
            return response.body ?? new CourseAssigment();
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

  getAllCourseAssignments(): Observable<CourseAssigment[]> {
    return this.client
      .get<CourseAssigment[]>(this.apiUrl + 'GetAllCourseAssigments', {
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
}
