import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
} from '@angular/router';
import { Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { isPlatformBrowser } from '@angular/common';
import { Inject, PLATFORM_ID } from '@angular/core';
import { CustomJwtPayload } from '../Domain/jwtPayload';

@Injectable({
  providedIn: 'root',
})
export class AdminGuard implements CanActivate {
  constructor(
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean> | boolean {
    if (!isPlatformBrowser(this.platformId)) return false;
    const token = localStorage.getItem('jwtToken');
    if (token == null) return false;
    var payload = jwtDecode<CustomJwtPayload>(token);
    if (!payload.role) return false;

    if (payload.role == 'Admin') return true;
    else {
      this.router.navigate(['/login']);
      return false;
    }
  }
}
