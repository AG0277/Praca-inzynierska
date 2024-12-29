import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

export function handleError(error: HttpErrorResponse) {
  if (error.status === 404) {
    console.error('Not Found (404)', error.message);
  } else if (error.status === 500) {
    console.error('Internal Server Error (500)', error.message);
  } else if (error.status === 400) {
    console.error('Bad Request (400)', error.message);
  } else {
    console.error('An unexpected error occurred:', error.message);
  }

  return throwError('Something went wrong. Please try again later.');
}

export function convertToUTCWithoutShift(date: Date): Date {
  // Get individual components from the local date
  const year = date.getFullYear();
  const month = date.getMonth(); // Month is 0-indexed
  const day = date.getDate();
  const hours = date.getHours();
  const minutes = date.getMinutes();
  const seconds = date.getSeconds();
  const milliseconds = date.getMilliseconds();

  // Create a new Date object in UTC with the same components
  return new Date(
    Date.UTC(year, month, day, hours, minutes, seconds, milliseconds)
  );
}
