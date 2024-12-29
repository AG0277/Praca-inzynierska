import { Routes } from '@angular/router';
import { RoomsComponent } from './pages/rooms/rooms.component';
import { CoursesComponent } from './pages/courses/courses.component';
import { InstructorsComponent } from './pages/instructors/instructors.component';
import { CourseAssignementsComponent } from './pages/course-assignements/course-assignements.component';
import { UserReservationsComponent } from './pages/user-reservations/user-reservations.component';
import { LoginComponent } from './pages/login/login.component';
import { AuthGuard } from './services/auth.guard';
import { RegisterComponent } from './pages/register/register.component';
import { AdminGuard } from './services/admin.guard';

export const routes: Routes = [
  {
    path: '',
    component: RoomsComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'rooms',
    component: RoomsComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'courses',
    component: CoursesComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'instructors',
    component: InstructorsComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'assignements',
    component: CourseAssignementsComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'reservations',
    component: UserReservationsComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
    canActivate: [AdminGuard],
  },
];
