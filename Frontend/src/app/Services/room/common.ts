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
