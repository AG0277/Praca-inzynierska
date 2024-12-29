import { Routes } from '@angular/router';
import { HomeComponent } from './Pages/home/home.component';
import { BookingComponent } from './Pages/booking/booking.component';
import { CoursesScheduleComponent } from './Pages/courses-schedule/courses-schedule.component';
import { TermsOfUseComponent } from './Pages/terms-of-use/terms-of-use.component';
import { PayUComponent } from './Pages/pay-u/pay-u.component';
import { CoursesComponent } from './Pages/courses/courses.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'booking',
    component: BookingComponent,
  },
  {
    path: 'kursy',
    component: CoursesComponent,
  },
  {
    path: 'harmonogram',
    component: CoursesScheduleComponent,
  },
  {
    path: 'regulamin',
    component: TermsOfUseComponent,
  },
  {
    path: 'payU',
    component: PayUComponent,
  },
];
