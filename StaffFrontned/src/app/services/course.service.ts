import {
  HttpClient,
  HttpErrorResponse,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of, throwError } from 'rxjs';
import { environment } from '../../environment';
import { Course, CreateCourseDto, UpdateCourseDto } from '../Domain/course';

@Injectable({
  providedIn: 'root',
})
export class CourseService {
  private apiUrl = environment.apiUrl + 'Course/';
  constructor(private client: HttpClient) {}
  getAllCourses(): Observable<Course[]> {
    return this.client
      .get<Course[]>(this.apiUrl + 'GetAllCourses', { observe: 'response' })
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

  getCourseById(id: number): Observable<Course> {
    return this.client
      .get<Course[]>(this.apiUrl + `GetCourseById/${id}`, {
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
          if (error.status === 404) return of(new Course(0, '', '', 0, '', 0));
          console.error('Error in HTTP request:', error);
          return throwError(
            () => new Error('Something went wrong. Please try again later.')
          );
        })
      );
  }

  updateCourse(updateCourseDto: UpdateCourseDto): Observable<boolean> {
    return this.client
      .put<boolean>(this.apiUrl + 'UpdateCourse', updateCourseDto, {
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

  createCourse(course: CreateCourseDto): Observable<Course> {
    return this.client
      .post<Course>(this.apiUrl + 'CreateCourse', course, {
        observe: 'response',
      })
      .pipe(
        map((response: HttpResponse<Course>) => {
          if (response.status === 200) {
            return response.body ?? new Course(0, '', '', 0, '', 0);
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

  deleteCourse(id: number): Observable<boolean> {
    return this.client
      .delete<boolean>(this.apiUrl + `DeleteCourse/${id}`, {
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
